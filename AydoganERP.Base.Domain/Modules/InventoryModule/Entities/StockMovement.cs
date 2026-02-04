using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.InventoryModule.Enums;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

public class StockMovement : Entity
{
    // For EF
    public StockMovement() { }
    
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; set; }

    public DateOnly Date { get; private set; }
    public int Type { get; private set; }

    /// <summary>
    /// Signed quantity change:
    /// + => stock increases
    /// - => stock decreases
    /// </summary>
    public decimal QuantityDelta { get; private set; }

    public string? ReferenceType { get; private set; } // "Sale", "Purchase", "StockCount"
    public Guid? ReferenceId { get; private set; }
    public string? ReferenceNo { get; private set; }

    public string Description { get; private set; } = string.Empty;
    
    public static StockMovement Create(
        Guid id,
        Guid productId,
        DateOnly date,
        int type,
        decimal quantityDelta,
        string? description = null,
        string? referenceType = null,
        Guid? referenceId = null,
        string? referenceNo = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (productId == Guid.Empty) throw new ArgumentException("ProductId cannot be empty.");
        if (quantityDelta == 0) throw new ArgumentException("QuantityDelta cannot be 0.");

        // Type bazlı temel doğrulamalar (başlangıç sürümü)
        if (type == StockMovementTypeEnum.PurchaseIn && quantityDelta < 0)
            throw new InvalidOperationException("Purchase movement must increase stock (QuantityDelta > 0).");

        if (type == StockMovementTypeEnum.SaleOut && quantityDelta > 0)
            throw new InvalidOperationException("Sale movement must decrease stock (QuantityDelta < 0).");

        if (type == StockMovementTypeEnum.Opening && quantityDelta < 0)
            throw new InvalidOperationException("Opening balance cannot be negative.");

        // Adjustment hem + hem - olabilir (sayım farkı)
        return new StockMovement
        {
            Id = id,
            ProductId = productId,
            Date = date,
            Type = type,
            QuantityDelta = quantityDelta,
            Description = (description ?? string.Empty).Trim(),
            ReferenceType = referenceType,
            ReferenceId = referenceId,
            ReferenceNo = referenceNo?.Trim(),
        };
    }
}