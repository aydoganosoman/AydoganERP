using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

public class ProductBarcode : Entity
{
    // For EF
    public ProductBarcode() { }

    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public string Barcode { get; private set; } = default!;
    public decimal Quantity { get; private set; } = 1;
    public string Unit { get; private set; } = "Adet";

    public static ProductBarcode Create(
        Guid id,
        Guid productId,
        string barcode,
        decimal quantity = 1,
        string unit = "Adet")
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (productId == Guid.Empty) throw new ArgumentException("ProductId cannot be empty.");
        if (string.IsNullOrWhiteSpace(barcode)) throw new ArgumentException("Barcode is required.");
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0.");

        return new ProductBarcode
        {
            Id = id,
            ProductId = productId,
            Barcode = barcode.Trim(),
            Quantity = quantity,
            Unit = unit?.Trim() ?? "Adet"
        };
    }
}
