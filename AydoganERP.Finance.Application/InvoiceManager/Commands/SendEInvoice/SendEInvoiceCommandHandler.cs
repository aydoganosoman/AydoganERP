using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using AydoganERP.Finance.Application.EInvoice;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AydoganERP.Finance.Application.InvoiceManager.Commands.SendEInvoice;

public class SendEInvoiceCommandHandler : IRequestHandler<SendEInvoiceCommand, SendEInvoiceResult>
{
    private readonly IBaseDbContext _dbContext;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IEInvoiceProvider _eInvoiceProvider;
    private readonly ILogger<SendEInvoiceCommandHandler> _logger;

    public SendEInvoiceCommandHandler(
        IBaseDbContext dbContext,
        IDomainEventUnitOfWork unitOfWork,
        IEInvoiceProvider eInvoiceProvider,
        ILogger<SendEInvoiceCommandHandler> logger)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _eInvoiceProvider = eInvoiceProvider;
        _logger = logger;
    }

    public async Task<SendEInvoiceResult> Handle(SendEInvoiceCommand request, CancellationToken cancellationToken)
    {
        // Fatura ve müşteri bilgilerini çek
        var invoice = await _dbContext.Invoices
            .Include(i => i.Lines)
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);

        if (invoice == null)
        {
            return new SendEInvoiceResult
            {
                Success = false,
                ErrorMessage = "Fatura bulunamadı"
            };
        }

        // Sadece onaylı faturalar e-fatura olarak gönderilebilir
        if (invoice.Status != InvoiceStatusEnum.Approved)
        {
            return new SendEInvoiceResult
            {
                Success = false,
                ErrorMessage = "Sadece onaylı faturalar e-fatura olarak gönderilebilir"
            };
        }

        // Zaten e-fatura gönderilmiş mi kontrol et
        if (invoice.IsEInvoice && !string.IsNullOrEmpty(invoice.EInvoiceUUID))
        {
            return new SendEInvoiceResult
            {
                Success = false,
                ErrorMessage = "Bu fatura zaten e-fatura olarak gönderilmiş"
            };
        }

        // E-Fatura log oluştur
        var eInvoiceLog = EInvoiceLog.Create(
            Guid.NewGuid(),
            invoice.Id,
            _eInvoiceProvider.ProviderName);

        await _dbContext.EInvoiceLogs.AddAsync(eInvoiceLog, cancellationToken);

        try
        {
            // E-Fatura isteği hazırla
            var eInvoiceRequest = BuildEInvoiceRequest(invoice);

            // E-Fatura gönder
            eInvoiceLog.MarkAsSending(string.Empty);
            var result = await _eInvoiceProvider.SendInvoiceAsync(eInvoiceRequest, cancellationToken);

            if (result.Success && !string.IsNullOrEmpty(result.EInvoiceUUID))
            {
                eInvoiceLog.MarkAsSent(result.EInvoiceUUID, result.ProviderResponse ?? string.Empty);
                invoice.SetEInvoiceUUID(result.EInvoiceUUID);

                await _unitOfWork.CommitAsync(_dbContext, cancellationToken);

                _logger.LogInformation(
                    "E-Fatura gönderildi: {InvoiceNumber} -> {EInvoiceUUID}",
                    invoice.InvoiceNumber,
                    result.EInvoiceUUID);

                return new SendEInvoiceResult
                {
                    Success = true,
                    EInvoiceLogId = eInvoiceLog.Id,
                    EInvoiceUUID = result.EInvoiceUUID
                };
            }
            else
            {
                eInvoiceLog.MarkAsError(result.ErrorMessage ?? "Bilinmeyen hata");
                await _unitOfWork.CommitAsync(_dbContext, cancellationToken);

                _logger.LogWarning(
                    "E-Fatura gönderilemedi: {InvoiceNumber} - {Error}",
                    invoice.InvoiceNumber,
                    result.ErrorMessage);

                return new SendEInvoiceResult
                {
                    Success = false,
                    EInvoiceLogId = eInvoiceLog.Id,
                    ErrorMessage = result.ErrorMessage
                };
            }
        }
        catch (Exception ex)
        {
            eInvoiceLog.MarkAsError(ex.Message);
            await _unitOfWork.CommitAsync(_dbContext, cancellationToken);

            _logger.LogError(ex, "E-Fatura gönderim hatası: {InvoiceNumber}", invoice.InvoiceNumber);

            return new SendEInvoiceResult
            {
                Success = false,
                EInvoiceLogId = eInvoiceLog.Id,
                ErrorMessage = ex.Message
            };
        }
    }

    private static EInvoiceRequest BuildEInvoiceRequest(Invoice invoice)
    {
        var request = new EInvoiceRequest
        {
            InvoiceId = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            InvoiceDate = invoice.InvoiceDate,
            InvoiceTypeCode = invoice.InvoiceType == InvoiceTypeEnum.SalesInvoice ? "SATIS" : "IADE",
            SubTotal = invoice.SubTotal,
            VatTotal = invoice.VatTotal,
            DiscountTotal = invoice.DiscountTotal,
            GrandTotal = invoice.GrandTotal,
            CurrencyCode = GetCurrencyCode(invoice.Currency),
            Notes = invoice.Notes
        };

        // Müşteri bilgileri
        if (invoice.Customer != null)
        {
            request.CustomerName = invoice.Customer.Name;
            // TaxInfo ve Address varsa doldur
        }

        // Satırlar
        foreach (var line in invoice.Lines)
        {
            request.Lines.Add(new EInvoiceLineRequest
            {
                LineNumber = line.LineNumber,
                ProductCode = line.ProductCode ?? string.Empty,
                ProductName = line.ProductName,
                Quantity = line.Quantity,
                UnitCode = line.UnitName ?? "C62",
                UnitPrice = line.UnitPrice,
                VatRate = (decimal)line.VatRate,
                VatAmount = line.VatAmount,
                DiscountAmount = line.DiscountAmount,
                LineTotal = line.LineTotal
            });
        }

        return request;
    }

    private static string GetCurrencyCode(int currency)
    {
        return currency switch
        {
            0 => "TRY",
            1 => "USD",
            2 => "EUR",
            _ => "TRY"
        };
    }
}
