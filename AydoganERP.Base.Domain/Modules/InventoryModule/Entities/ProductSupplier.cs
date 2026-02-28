using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

public class ProductSupplier : Entity
{
    // For EF
    public ProductSupplier() { }

    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public static ProductSupplier Create(
        Guid id,
        Guid productId,
        Guid customerId,
        string code,
        string name)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (productId == Guid.Empty) throw new ArgumentException("ProductId cannot be empty.");
        if (customerId == Guid.Empty) throw new ArgumentException("CustomerId cannot be empty.");
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code is required.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        return new ProductSupplier
        {
            Id = id,
            ProductId = productId,
            CustomerId = customerId,
            Code = code.Trim(),
            Name = name.Trim()
        };
    }
}
