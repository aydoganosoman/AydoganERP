using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.InventoryModule.Events;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

public class Product : Entity
{
    // For EF
    public Product() { }
    
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public Guid UnitId { get; private set; }
    public ProductUnit Unit { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }
    // Alış Fiyat Bilgileri (satış fiyatları ProductUnitPrice'da)
    public decimal PurchaseUnitPrice { get; private set; }
    public int PurchaseUnitPriceCurrency { get; private set; }
    public bool PurchaseUnitPriceVatInclude { get; private set; }
    public float PurchaseVatRate { get; private set; } // 0, 1, 10, 20 vs
    
    public bool IsLotTracked { get; set; }
    public bool IsSerialTracked { get; private set; }
    public bool IsActive { get; private set; } = true;

    /// <summary>
    /// Birim fiyatları ve barkodlar - her birimin kendi fiyatı/barkodu olabilir
    /// </summary>
    public List<ProductUnitPrice> UnitPrices { get; private set; } = new();
    public List<ProductSupplier> ProductSuppliers { get; private set; } = new();
    public List<ProductSerialNumber> SerialNumbers { get; private set; } = new();
    public List<StockMovement> Movements { get; private set; } = new();
    public List<StockBatch> StockBatches { get; private set; } = new();
    
    public static Product Create(
        Guid id,
        Guid companyId,
        string code,
        string name,
        Guid unitId,
        Guid? categoryId = null,
        decimal purchaseUnitPrice = 0,
        int purchaseUnitPriceCurrency = 0,
        bool purchaseUnitPriceVatInclude = false,
        float purchaseVatRate = 0,
        bool isLotTracked = false,
        bool isSerialTracked = false)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code is required.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        if (purchaseVatRate < 0 || purchaseVatRate > 100) throw new ArgumentException("PurchaseVatRate must be 0..100");

        var product = new Product
        {
            Id = id,
            CompanyId = companyId,
            Code = code.Trim(),
            Name = name.Trim(),
            UnitId = unitId,
            CategoryId = categoryId,
            PurchaseUnitPrice = purchaseUnitPrice,
            PurchaseUnitPriceCurrency = purchaseUnitPriceCurrency,
            PurchaseUnitPriceVatInclude = purchaseUnitPriceVatInclude,
            PurchaseVatRate = purchaseVatRate,
            IsLotTracked = isLotTracked,
            IsSerialTracked = isSerialTracked,
            IsActive = true
        };

        product.PublishEvent(new ProductCreatedEvent(product.Id, product.CompanyId, product.Code, product.Name));

        return product;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        Name = name.Trim();
    }

    public void SetActive(bool active) => IsActive = active;

    public void SetCategory(Guid? categoryId) => CategoryId = categoryId;

    public void SetUnit(Guid unitId) => UnitId = unitId;

    public void SetLotTracked(bool isLotTracked) => IsLotTracked = isLotTracked;

    public void SetSerialTracked(bool isSerialTracked) => IsSerialTracked = isSerialTracked;

    public void SetPurchasePricing(decimal unitPrice, int currency, bool vatInclude, float vatRate)
    {
        if (vatRate < 0 || vatRate > 100) throw new ArgumentException("PurchaseVatRate must be 0..100");
        PurchaseUnitPrice = unitPrice;
        PurchaseUnitPriceCurrency = currency;
        PurchaseUnitPriceVatInclude = vatInclude;
        PurchaseVatRate = vatRate;
    }

    public void AddUnitPrice(ProductUnitPrice unitPrice)
    {
        // Eğer ana birim olarak ekleniyor ve zaten ana birim varsa, eskisini kaldır
        if (unitPrice.IsBaseUnit)
        {
            foreach (var up in UnitPrices.Where(x => x.IsBaseUnit))
            {
                up.Update(up.ConversionRate, up.Barcode, up.SaleUnitPrice, up.SaleUnitPriceCurrency, 
                    up.SaleUnitPriceVatInclude, up.SaleVatRate, false);
            }
        }
        UnitPrices.Add(unitPrice);
    }

    public void RemoveUnitPrice(Guid unitPriceId)
    {
        var unitPrice = UnitPrices.FirstOrDefault(x => x.Id == unitPriceId);
        if (unitPrice != null)
        {
            UnitPrices.Remove(unitPrice);
        }
    }

    public void ClearUnitPrices()
    {
        UnitPrices.Clear();
    }
}
