using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

public class Product : Entity
{
    // For EF
    public Product() { }
    
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; }
    public Guid SupplierId { get; private set; }
    public Customer Supplier { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Unit { get; private set; } = "Adet";
    public decimal VatRate { get; private set; } // 0, 1, 10, 20 vs
    public bool IsActive { get; private set; } = true;

    public List<StockMovement> Movements { get; private set; } = new();
    
    public static Product Create(Guid id, string code, string name, string unit = "Adet", decimal vatRate = 20)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code is required.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        if (string.IsNullOrWhiteSpace(unit)) unit = "Adet";
        if (vatRate < 0 || vatRate > 100) throw new ArgumentException("VatRate must be 0..100");

        return new Product
        {
            Id = id,
            Code = code.Trim(),
            Name = name.Trim(),
            Unit = unit.Trim(),
            VatRate = vatRate,
            IsActive = true
        };
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        Name = name.Trim();
    }

    public void SetActive(bool active) => IsActive = active;
}