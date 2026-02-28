using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

public class StockBatch : Entity
{
    // For EF
    public StockBatch() { }

    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public string Code { get; private set; } = default!;
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime EntryDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }

    public static StockBatch Create(
        Guid id,
        Guid productId,
        string code,
        string? name = null,
        string? description = null,
        DateTime? entryDate = null,
        DateTime? expiryDate = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (productId == Guid.Empty) throw new ArgumentException("ProductId cannot be empty.");
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code is required.");

        return new StockBatch
        {
            Id = id,
            ProductId = productId,
            Code = code.Trim(),
            Name = name?.Trim(),
            Description = description?.Trim(),
            EntryDate = entryDate ?? DateTime.UtcNow,
            ExpiryDate = expiryDate
        };
    }
}
