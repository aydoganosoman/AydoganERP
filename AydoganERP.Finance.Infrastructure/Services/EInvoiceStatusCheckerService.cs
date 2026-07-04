using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using AydoganERP.Base.Infrastructure.Persistence;
using AydoganERP.EInvoice.Abstractions.Enums;
using AydoganERP.EInvoice.Abstractions.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AydoganERP.Finance.Infrastructure.Services;

/// <summary>
/// E-Fatura durumlarını periyodik olarak kontrol eden background service
/// </summary>
public class EInvoiceStatusCheckerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EInvoiceStatusCheckerService> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(10);

    public EInvoiceStatusCheckerService(
        IServiceProvider serviceProvider,
        ILogger<EInvoiceStatusCheckerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("EInvoiceStatusCheckerService başlatıldı. Kontrol aralığı: {Interval} dakika", _checkInterval.TotalMinutes);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckPendingInvoicesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "E-Fatura durum kontrolü sırasında hata oluştu");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task CheckPendingInvoicesAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var integratorFactory = scope.ServiceProvider.GetRequiredService<IntegratorFactory>();

        // EInvoiceSent durumundaki faturaları al
        var pendingInvoices = await context.Invoices
            .Include(i => i.Company)
            .Where(i => i.Status == InvoiceStatusEnum.EInvoiceSent &&
                        !string.IsNullOrEmpty(i.EInvoiceUUID))
            .ToListAsync(cancellationToken);

        if (!pendingInvoices.Any())
        {
            _logger.LogDebug("Kontrol edilecek bekleyen fatura bulunamadı");
            return;
        }

        _logger.LogInformation("Kontrol edilecek {Count} adet bekleyen fatura bulundu", pendingInvoices.Count);

        // Şirketlere göre grupla (her şirketin farklı entegrasyonu olabilir)
        var groupedByCompany = pendingInvoices.GroupBy(i => i.CompanyId);

        foreach (var companyGroup in groupedByCompany)
        {
            var companyId = companyGroup.Key;

            // Entegrasyon bilgisini al
            var integration = await context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.IsActive, cancellationToken);

            if (integration == null)
            {
                _logger.LogWarning("Şirket için aktif entegrasyon bulunamadı: {CompanyId}", companyId);
                continue;
            }

            var integrator = integratorFactory.Create(
                integration.IntegrationType,
                integration.Settings ?? "{}");

            foreach (var invoice in companyGroup)
            {
                try
                {
                    await CheckAndUpdateInvoiceStatusAsync(invoice, integrator, context, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fatura durumu kontrol hatası: {InvoiceId}", invoice.Id);
                }
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task CheckAndUpdateInvoiceStatusAsync(
        AydoganERP.Base.Domain.Modules.FinanceModule.Entities.Invoice invoice,
        AydoganERP.EInvoice.Abstractions.Interfaces.IEInvoiceIntegrator integrator,
        ApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var statusResult = await integrator.GetInvoiceStatusAsync(invoice.EInvoiceUUID!, isOutbox: true);

        if (!statusResult.Success)
        {
            _logger.LogWarning("Durum sorgusu başarısız: {InvoiceId}, Hata: {Error}",
                invoice.Id, statusResult.ErrorMessage);
            return;
        }

        var newStatus = statusResult.Status;
        var currentInvoiceStatus = invoice.Status;

        // Duruma göre Invoice'ı güncelle
        switch (newStatus)
        {
            case EInvoiceStatus.Accepted:
                if (currentInvoiceStatus != InvoiceStatusEnum.EInvoiceAccepted)
                {
                    invoice.SetEInvoiceAccepted();
                    _logger.LogInformation("Fatura kabul edildi: {InvoiceNumber}, ETTN: {ETTN}",
                        invoice.InvoiceNumber, invoice.EInvoiceUUID);
                }
                break;

            case EInvoiceStatus.Rejected:
                if (currentInvoiceStatus != InvoiceStatusEnum.EInvoiceRejected)
                {
                    invoice.SetEInvoiceRejected();
                    _logger.LogInformation("Fatura reddedildi: {InvoiceNumber}, ETTN: {ETTN}, Neden: {Reason}",
                        invoice.InvoiceNumber, invoice.EInvoiceUUID, statusResult.DeclineReason);
                }
                break;

            case EInvoiceStatus.Error:
                _logger.LogWarning("Fatura hata durumunda: {InvoiceNumber}, ETTN: {ETTN}, Hata: {Error}",
                    invoice.InvoiceNumber, invoice.EInvoiceUUID, statusResult.ErrorMessage);
                // Hata durumunda ne yapılacağına karar verilmeli
                break;

            case EInvoiceStatus.Cancelled:
                _logger.LogWarning("Fatura iptal edilmiş: {InvoiceNumber}, ETTN: {ETTN}",
                    invoice.InvoiceNumber, invoice.EInvoiceUUID);
                break;

            default:
                // Queued, SentToGIB, WaitingResponse, ReachedBuyer gibi ara durumlar
                _logger.LogDebug("Fatura hala işlemde: {InvoiceNumber}, Durum: {Status}",
                    invoice.InvoiceNumber, newStatus);
                break;
        }
    }
}
