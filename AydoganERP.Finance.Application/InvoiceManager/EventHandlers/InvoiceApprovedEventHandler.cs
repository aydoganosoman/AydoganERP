using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Models;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using AydoganERP.Base.Domain.Modules.FinanceModule.Events;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using Enums = AydoganERP.Base.Domain.Modules.InventoryModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AydoganERP.Finance.Application.InvoiceManager.EventHandlers;

/// <summary>
/// Fatura onaylandığında otomatik stok hareketi oluşturur.
/// - Satış Faturası: Stok çıkışı (negatif)
/// - Alış Faturası: Stok girişi (pozitif)
/// - Satış İade: Stok girişi (pozitif)
/// - Alış İade: Stok çıkışı (negatif)
/// </summary>
public class InvoiceApprovedEventHandler : INotificationHandler<DomainEventNotification<InvoiceApprovedEvent>>
{
    private readonly IBaseDbContext _dbContext;
    private readonly ILogger<InvoiceApprovedEventHandler> _logger;

    public InvoiceApprovedEventHandler(
        IBaseDbContext dbContext,
        ILogger<InvoiceApprovedEventHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(DomainEventNotification<InvoiceApprovedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        _logger.LogInformation("Processing stock movements for approved invoice {InvoiceNumber}", domainEvent.InvoiceNumber);

        // Faturayı satırlarıyla birlikte çek
        var invoice = await _dbContext.Invoices
            .Include(i => i.Lines)
            .FirstOrDefaultAsync(i => i.Id == domainEvent.InvoiceId, cancellationToken);

        if (invoice == null)
        {
            _logger.LogWarning("Invoice {InvoiceId} not found for stock movement creation", domainEvent.InvoiceId);
            return;
        }

        // Stok hareket tipini ve yönünü belirle
        var (movementType, multiplier) = GetMovementTypeAndMultiplier(invoice.InvoiceType);

        foreach (var line in invoice.Lines)
        {
            // Sadece ürün ID'si olan satırlar için stok hareketi oluştur
            if (!line.ProductId.HasValue)
            {
                _logger.LogDebug("Skipping line {LineNumber} - no product linked", line.LineNumber);
                continue;
            }

            var quantityDelta = line.Quantity * multiplier;

            var movement = StockMovement.Create(
                Guid.NewGuid(),
                line.ProductId.Value,
                DateOnly.FromDateTime(invoice.InvoiceDate),
                movementType,
                quantityDelta,
                $"Fatura: {invoice.InvoiceNumber} - Satır: {line.LineNumber}",
                "Invoice",
                invoice.Id,
                invoice.InvoiceNumber);

            await _dbContext.StockMovements.AddAsync(movement, cancellationToken);

            // Seri numaralı ürün ise seri numarası durumunu güncelle
            if (line.SerialNumberId.HasValue)
            {
                var serialNumber = await _dbContext.ProductSerialNumbers
                    .FirstOrDefaultAsync(s => s.Id == line.SerialNumberId.Value, cancellationToken);

                if (serialNumber != null)
                {
                    // Satış faturası veya satış iade ise durumu güncelle
                    if (invoice.InvoiceType == InvoiceTypeEnum.SalesInvoice)
                    {
                        serialNumber.SetSaleInfo(invoice.InvoiceDate, line.UnitPrice, invoice.CustomerId);
                    }
                    else if (invoice.InvoiceType == InvoiceTypeEnum.SalesReturn)
                    {
                        serialNumber.UpdateStatus(Enums.SerialNumberStatusEnum.Returned);
                    }
                    else if (invoice.InvoiceType == InvoiceTypeEnum.PurchaseInvoice)
                    {
                        serialNumber.UpdateStatus(Enums.SerialNumberStatusEnum.InStock);
                        serialNumber.SetPurchaseInfo(invoice.InvoiceDate, line.UnitPrice, invoice.CustomerId);
                    }
                }
            }

            _logger.LogDebug(
                "Created stock movement for product {ProductId}: {Quantity} (Invoice: {InvoiceNumber})",
                line.ProductId,
                quantityDelta,
                invoice.InvoiceNumber);
        }

        _logger.LogInformation(
            "Created {Count} stock movements for invoice {InvoiceNumber}",
            invoice.Lines.Count(l => l.ProductId.HasValue),
            invoice.InvoiceNumber);
    }

    private static (int MovementType, decimal Multiplier) GetMovementTypeAndMultiplier(int invoiceType)
    {
        return invoiceType switch
        {
            InvoiceTypeEnum.SalesInvoice => (Enums.StockMovementTypeEnum.SaleOut, -1m),      // Satış: Çıkış
            InvoiceTypeEnum.PurchaseInvoice => (Enums.StockMovementTypeEnum.PurchaseIn, 1m), // Alış: Giriş
            InvoiceTypeEnum.SalesReturn => (Enums.StockMovementTypeEnum.PurchaseIn, 1m),     // Satış İade: Giriş
            InvoiceTypeEnum.PurchaseReturn => (Enums.StockMovementTypeEnum.SaleOut, -1m),    // Alış İade: Çıkış
            _ => (Enums.StockMovementTypeEnum.Adjustment, 0m)
        };
    }
}
