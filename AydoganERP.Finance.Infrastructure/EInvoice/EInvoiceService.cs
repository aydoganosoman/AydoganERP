using AydoganERP.Base.Domain.Modules.CompanyModule.Enums;
using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using AydoganERP.Base.Infrastructure.Persistence;
using AydoganERP.EInvoice.Abstractions.Enums;
using AydoganERP.EInvoice.Abstractions.Interfaces;
using AydoganERP.EInvoice.Abstractions.Services;
using AydoganERP.Finance.Application.EInvoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// Alias for disambiguation
using EInvoiceModels = AydoganERP.EInvoice.Abstractions.Models;

namespace AydoganERP.Finance.Infrastructure.EInvoice;

/// <summary>
/// E-Fatura servis implementasyonu
/// </summary>
public class EInvoiceService : IEInvoiceService
{
    private readonly ApplicationDbContext _context;
    private readonly IntegratorFactory _integratorFactory;
    private readonly ILogger<EInvoiceService> _logger;

    public EInvoiceService(
        ApplicationDbContext context,
        IntegratorFactory integratorFactory,
        ILogger<EInvoiceService> logger)
    {
        _context = context;
        _integratorFactory = integratorFactory;
        _logger = logger;
    }

    public async Task<EInvoiceModels.EInvoiceResponse> SendInvoiceAsync(Guid invoiceId)
    {
        // 1. Faturayı al
        var invoice = await _context.Invoices
            .Include(i => i.Lines)
            .Include(i => i.Company)
            .Include(i => i.Customer)
            .Include(i=>i.Customer.Numbers)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        if (invoice == null)
        {
            return EInvoiceModels.EInvoiceResponse.CreateError("Fatura bulunamadı");
        }

        // 2. Fatura durumu kontrolü
        if (invoice.Status != InvoiceStatusEnum.Approved)
        {
            return EInvoiceModels.EInvoiceResponse.CreateError("Sadece onaylanmış faturalar gönderilebilir");
        }

        // 3. E-Fatura entegrasyon ayarlarını al
        var integration = await _context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
            .FirstOrDefaultAsync(e => e.CompanyId == invoice.CompanyId && e.IsActive);

        if (integration == null)
        {
            return EInvoiceModels.EInvoiceResponse.CreateError("Aktif e-fatura entegrasyonu bulunamadı");
        }

        // 4. Müşterinin e-fatura mükellefi olup olmadığını kontrol et
        // (GİB sorgusu yapılabilir, şimdilik IsEInvoice alanına bakıyoruz)
        var documentType = invoice.IsEInvoice
            ? EInvoiceDocumentType.EInvoice
            : EInvoiceDocumentType.EArchive;

        var scenario = invoice.IsEInvoice
            ? EInvoiceScenario.Commercial
            : EInvoiceScenario.EArchive;

        // 5. EInvoiceRequest oluştur
        var request = InvoiceMapper.ToEInvoiceRequest(
            invoice,
            invoice.Company,
            invoice.Customer,
            documentType,
            scenario);

        // 6. Log oluştur
        var log = EInvoiceLog.Create(
            Guid.NewGuid(),
            invoiceId,
            IntegratorCompanyTypeEnum.GetName(integration.IntegrationType));

        _context.Set<EInvoiceLog>().Add(log);

        try
        {
            // 7. Entegratör oluştur ve gönder
            var integrator = _integratorFactory.Create(
                integration.IntegrationType,
                integration.Settings ?? "{}");

            log.MarkAsSending(string.Empty); // UBL XML sonra eklenir

            var response = await integrator.SendInvoiceAsync(request);

            log.SetRequest(response.RawRequest ?? string.Empty);
            
            if (response.Success)
            {
                // 8. Başarılı - Invoice ve Log güncelle
                log.MarkAsSent(response.ETTN ?? string.Empty, response.RawResponse ?? string.Empty);
                invoice.SetEInvoiceUUID(response.ETTN ?? string.Empty);

                _logger.LogInformation(
                    "E-Fatura gönderildi: InvoiceId={InvoiceId}, ETTN={ETTN}",
                    invoiceId, response.ETTN);
            }
            else
            {
                // 9. Hata - Log güncelle
                log.MarkAsError(response.ErrorMessage ?? "Bilinmeyen hata");

                _logger.LogWarning(
                    "E-Fatura gönderim hatası: InvoiceId={InvoiceId}, Hata={Error}",
                    invoiceId, response.ErrorMessage);
            }

            await _context.SaveChangesAsync();
            return response;
        }
        catch (Exception ex)
        {
            log.MarkAsError(ex.Message);
            await _context.SaveChangesAsync();

            _logger.LogError(ex, "E-Fatura gönderim exception: InvoiceId={InvoiceId}", invoiceId);
            return EInvoiceModels.EInvoiceResponse.CreateError(ex.Message);
        }
    }

    public async Task<EInvoiceModels.EInvoiceStatusResult> GetStatusAsync(Guid invoiceId)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Company)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        if (invoice == null || string.IsNullOrEmpty(invoice.EInvoiceUUID))
        {
            return new EInvoiceModels.EInvoiceStatusResult
            {
                Success = false,
                ErrorMessage = "Fatura bulunamadı veya e-fatura UUID mevcut değil"
            };
        }

        var integration = await _context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
            .FirstOrDefaultAsync(e => e.CompanyId == invoice.CompanyId && e.IsActive);

        if (integration == null)
        {
            return new EInvoiceModels.EInvoiceStatusResult
            {
                Success = false,
                ErrorMessage = "Aktif e-fatura entegrasyonu bulunamadı"
            };
        }

        var integrator = _integratorFactory.Create(
            integration.IntegrationType,
            integration.Settings ?? "{}");

        var result = await integrator.GetInvoiceStatusAsync(invoice.EInvoiceUUID, isOutbox: true);

        // Duruma göre Invoice güncelle
        if (result.Success)
        {
            switch (result.Status)
            {
                case EInvoiceStatus.Accepted:
                    invoice.SetEInvoiceAccepted();
                    break;
                case EInvoiceStatus.Rejected:
                    invoice.SetEInvoiceRejected();
                    break;
            }
            await _context.SaveChangesAsync();
        }

        return result;
    }

    public async Task<byte[]> GetPdfAsync(Guid invoiceId)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Company)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        if (invoice == null || string.IsNullOrEmpty(invoice.EInvoiceUUID))
        {
            throw new InvalidOperationException("Fatura bulunamadı veya e-fatura UUID mevcut değil");
        }

        var integration = await _context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
            .FirstOrDefaultAsync(e => e.CompanyId == invoice.CompanyId && e.IsActive);

        if (integration == null)
        {
            throw new InvalidOperationException("Aktif e-fatura entegrasyonu bulunamadı");
        }

        var integrator = _integratorFactory.Create(
            integration.IntegrationType,
            integration.Settings ?? "{}");

        return await integrator.GetInvoicePdfAsync(invoice.EInvoiceUUID, isOutbox: true);
    }

    public async Task<string> GetXmlAsync(Guid invoiceId)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Company)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        if (invoice == null || string.IsNullOrEmpty(invoice.EInvoiceUUID))
        {
            throw new InvalidOperationException("Fatura bulunamadı veya e-fatura UUID mevcut değil");
        }

        var integration = await _context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
            .FirstOrDefaultAsync(e => e.CompanyId == invoice.CompanyId && e.IsActive);

        if (integration == null)
        {
            throw new InvalidOperationException("Aktif e-fatura entegrasyonu bulunamadı");
        }

        var integrator = _integratorFactory.Create(
            integration.IntegrationType,
            integration.Settings ?? "{}");

        return await integrator.GetInvoiceXmlAsync(invoice.EInvoiceUUID, isOutbox: true);
    }

    public async Task<EInvoiceModels.GibAccount?> GetGibAccountAsync(Guid companyId, string taxNumber)
    {
        var integration = await _context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
            .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.IsActive);

        if (integration == null)
        {
            _logger.LogWarning("GİB sorgusu için aktif entegrasyon bulunamadı: CompanyId={CompanyId}", companyId);
            return null;
        }

        var integrator = _integratorFactory.Create(
            integration.IntegrationType,
            integration.Settings ?? "{}");

        return await integrator.GetGibAccountAsync(taxNumber);
    }

    public async Task<IncomingSyncResult> SyncIncomingInvoicesAsync(Guid companyId, DateTime startDate, DateTime endDate)
    {
        var failedInvoices = new List<string>();

        var integration = await _context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
            .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.IsActive);

        if (integration == null)
        {
            _logger.LogWarning("Gelen fatura senkronizasyonu için aktif entegrasyon bulunamadı");
            return new IncomingSyncResult(0, 0, failedInvoices, "Aktif entegrasyon bulunamadı");
        }

        var integrator = _integratorFactory.Create(
            integration.IntegrationType,
            integration.Settings ?? "{}");

        var incomingInvoices = await integrator.GetIncomingInvoicesAsync(startDate, endDate);

        if (!incomingInvoices.Any())
        {
            _logger.LogInformation("Gelen fatura bulunamadı: CompanyId={CompanyId}, Tarih={StartDate}-{EndDate}",
                companyId, startDate, endDate);
            return new IncomingSyncResult(0, 0, failedInvoices, null);
        }

        // Mevcut ETTN'leri al (tekrarı önlemek için)
        var existingUUIDs = await _context.Invoices
            .Where(i => i.CompanyId == companyId && i.EInvoiceUUID != null)
            .Select(i => i.EInvoiceUUID)
            .ToListAsync();

        var existingUUIDSet = new HashSet<string>(existingUUIDs.Where(u => u != null)!);

        int savedCount = 0;
        foreach (var incoming in incomingInvoices)
        {
            // Daha önce kaydedilmişse atla
            if (existingUUIDSet.Contains(incoming.ETTN))
            {
                _logger.LogDebug("Gelen fatura zaten mevcut, atlanıyor: {ETTN}", incoming.ETTN);
                continue;
            }

            try
            {
                // Tedarikçiyi bul veya oluştur
                var supplier = await FindOrCreateSupplierAsync(companyId, incoming);

                // Para birimi dönüşümü
                int currency = incoming.Currency switch
                {
                    CurrencyType.TRY => 0,
                    CurrencyType.USD => 1,
                    CurrencyType.EUR => 2,
                    CurrencyType.GBP => 3,
                    _ => 0
                };

                // Ticari fatura mı kontrol et (Scenario = Commercial ise kabul bekleniyor)
                bool isCommercial = incoming.Scenario == EInvoiceScenario.Commercial;

                // Invoice entity oluştur
                var invoice = Invoice.CreateFromIncoming(
                    id: Guid.NewGuid(),
                    companyId: companyId,
                    supplierId: supplier.Id,
                    invoiceNumber: incoming.InvoiceNumber,
                    invoiceDate: incoming.InvoiceDate,
                    eInvoiceUUID: incoming.ETTN,
                    subTotal: incoming.TaxableAmount,
                    vatTotal: incoming.VatTotal,
                    grandTotal: incoming.GrandTotal,
                    currency: currency,
                    exchangeRate: incoming.ExchangeRate,
                    isCommercialInvoice: isCommercial);

                _context.Invoices.Add(invoice);
                savedCount++;

                _logger.LogDebug("Gelen fatura kaydedildi: {ETTN}, Tedarikçi={Supplier}",
                    incoming.ETTN, incoming.SenderTitle);
            }
            catch (Exception ex)
            {
                failedInvoices.Add($"{incoming.ETTN}: {ex.Message}");
                _logger.LogError(ex, "Gelen fatura kaydetme hatası: {ETTN}", incoming.ETTN);
            }
        }

        if (savedCount > 0)
        {
            await _context.SaveChangesAsync();
        }

        _logger.LogInformation(
            "Gelen fatura senkronizasyonu tamamlandı: CompanyId={CompanyId}, Toplam={Total}, Yeni={New}",
            companyId, incomingInvoices.Count, savedCount);

        return new IncomingSyncResult(savedCount, incomingInvoices.Count, failedInvoices, null);
    }

    /// <summary>
    /// Gelen faturadaki tedarikçiyi bul veya oluştur
    /// </summary>
    private async Task<AydoganERP.Base.Domain.Modules.CustomerModule.Entities.Customer> FindOrCreateSupplierAsync(
        Guid companyId,
        EInvoiceModels.IncomingInvoice incoming)
    {
        // Vergi numarasına göre mevcut tedarikçiyi ara
        var existingSupplier = await _context.Set<AydoganERP.Base.Domain.Modules.CustomerModule.Entities.Customer>()
            .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.TaxInfo.TaxNumber == incoming.SenderTaxNumber);

        if (existingSupplier != null)
        {
            return existingSupplier;
        }

        // Kod oluştur (VKN veya TCKN'nin son 6 hanesi)
        var code = $"S-{incoming.SenderTaxNumber.Substring(Math.Max(0, incoming.SenderTaxNumber.Length - 6))}";

        // TaxInfo oluştur
        var taxInfo = new AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects.TaxInfo(
            incoming.SenderTaxNumber,
            null); // Vergi dairesi bilgisi gelen faturada yok

        // Yeni tedarikçi oluştur
        var newSupplier = AydoganERP.Base.Domain.Modules.CustomerModule.Entities.Customer.Create(
            id: Guid.NewGuid(),
            companyId: companyId,
            code: code,
            customerName: incoming.SenderTitle,
            name: null,
            surName: null,
            type: AydoganERP.Base.Domain.Modules.CustomerModule.Enums.CustomerTypeEnum.Suplier,
            partyType: AydoganERP.Base.Domain.Modules.CustomerModule.Enums.PartyTypeEnum.Entity,
            taxInfo: taxInfo);

        _context.Set<AydoganERP.Base.Domain.Modules.CustomerModule.Entities.Customer>().Add(newSupplier);

        _logger.LogInformation("Yeni tedarikçi oluşturuldu: {TaxNumber} - {Title}",
            incoming.SenderTaxNumber, incoming.SenderTitle);

        return newSupplier;
    }

    public async Task<bool> AcceptIncomingInvoiceAsync(Guid invoiceId)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Company)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        if (invoice == null || string.IsNullOrEmpty(invoice.EInvoiceUUID))
        {
            _logger.LogWarning("Gelen fatura kabul - fatura bulunamadı: {InvoiceId}", invoiceId);
            return false;
        }

        // Sadece gelen faturalar kabul edilebilir
        if (!invoice.IsIncomingInvoice)
        {
            _logger.LogWarning("Sadece gelen faturalar kabul edilebilir: {InvoiceId}", invoiceId);
            return false;
        }

        var integration = await _context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
            .FirstOrDefaultAsync(e => e.CompanyId == invoice.CompanyId && e.IsActive);

        if (integration == null)
        {
            _logger.LogWarning("Aktif e-fatura entegrasyonu bulunamadı: CompanyId={CompanyId}", invoice.CompanyId);
            return false;
        }

        var integrator = _integratorFactory.Create(
            integration.IntegrationType,
            integration.Settings ?? "{}");

        // Entegratöre kabul gönder
        var result = await integrator.AcceptInvoiceAsync(invoice.EInvoiceUUID);

        if (result)
        {
            // Başarılı ise Invoice durumunu güncelle
            invoice.AcceptIncomingInvoice();
            await _context.SaveChangesAsync();

            _logger.LogInformation("Gelen fatura kabul edildi: {InvoiceId}, ETTN={ETTN}",
                invoiceId, invoice.EInvoiceUUID);
        }

        return result;
    }

    public async Task<bool> RejectIncomingInvoiceAsync(Guid invoiceId, string reason)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Company)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        if (invoice == null || string.IsNullOrEmpty(invoice.EInvoiceUUID))
        {
            _logger.LogWarning("Gelen fatura red - fatura bulunamadı: {InvoiceId}", invoiceId);
            return false;
        }

        // Sadece gelen faturalar reddedilebilir
        if (!invoice.IsIncomingInvoice)
        {
            _logger.LogWarning("Sadece gelen faturalar reddedilebilir: {InvoiceId}", invoiceId);
            return false;
        }

        var integration = await _context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
            .FirstOrDefaultAsync(e => e.CompanyId == invoice.CompanyId && e.IsActive);

        if (integration == null)
        {
            _logger.LogWarning("Aktif e-fatura entegrasyonu bulunamadı: CompanyId={CompanyId}", invoice.CompanyId);
            return false;
        }

        var integrator = _integratorFactory.Create(
            integration.IntegrationType,
            integration.Settings ?? "{}");

        // Entegratöre red gönder
        var result = await integrator.RejectInvoiceAsync(invoice.EInvoiceUUID, reason);

        if (result)
        {
            // Başarılı ise Invoice durumunu güncelle
            invoice.RejectIncomingInvoice(reason);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Gelen fatura reddedildi: {InvoiceId}, ETTN={ETTN}, Neden={Reason}",
                invoiceId, invoice.EInvoiceUUID, reason);
        }

        return result;
    }

    public async Task<bool> CancelEArchiveAsync(Guid invoiceId, string reason)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Company)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        if (invoice == null || string.IsNullOrEmpty(invoice.EInvoiceUUID))
        {
            return false;
        }

        var integration = await _context.Set<AydoganERP.Base.Domain.Modules.CompanyModule.Entities.EInvoiceIntegration>()
            .FirstOrDefaultAsync(e => e.CompanyId == invoice.CompanyId && e.IsActive);

        if (integration == null)
        {
            return false;
        }

        var integrator = _integratorFactory.Create(
            integration.IntegrationType,
            integration.Settings ?? "{}");

        return await integrator.CancelEArchiveAsync(
            invoice.EInvoiceUUID,
            DateTime.Now,
            "IPTAL",
            reason);
    }
}
