using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

/// <summary>
/// Ürün birim fiyatı - Her ürünün birden fazla birimi ve her birimin kendi fiyatı/barkodu olabilir
/// </summary>
public class ProductUnitPrice : Entity
{
    // For EF
    public ProductUnitPrice() { }

    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    
    public Guid UnitId { get; private set; }
    public ProductUnit Unit { get; private set; } = null!;
    
    /// <summary>
    /// Ana birime dönüşüm oranı. Örn: 1 Koli = 12 Adet ise ConversionRate = 12
    /// Ana birim için ConversionRate = 1
    /// </summary>
    public decimal ConversionRate { get; private set; } = 1;
    
    /// <summary>
    /// Bu birime ait barkod (opsiyonel)
    /// </summary>
    public string? Barcode { get; private set; }
    
    /// <summary>
    /// Satış birim fiyatı
    /// </summary>
    public decimal SaleUnitPrice { get; private set; }
    
    /// <summary>
    /// Para birimi (0=TRY, 1=USD, 2=EUR)
    /// </summary>
    public int SaleUnitPriceCurrency { get; private set; }
    
    /// <summary>
    /// KDV dahil mi?
    /// </summary>
    public bool SaleUnitPriceVatInclude { get; private set; }
    
    /// <summary>
    /// KDV oranı (0, 1, 10, 20 vs)
    /// </summary>
    public float SaleVatRate { get; private set; }
    
    /// <summary>
    /// Ana birim mi? Her üründe en az bir ana birim olmalı (ConversionRate = 1)
    /// </summary>
    public bool IsBaseUnit { get; private set; }
    
    public bool IsActive { get; private set; } = true;

    public static ProductUnitPrice Create(
        Guid id,
        Guid productId,
        Guid unitId,
        decimal conversionRate,
        string? barcode = null,
        decimal saleUnitPrice = 0,
        int saleUnitPriceCurrency = 0,
        bool saleUnitPriceVatInclude = false,
        float saleVatRate = 0,
        bool isBaseUnit = false)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (productId == Guid.Empty) throw new ArgumentException("ProductId cannot be empty.");
        if (unitId == Guid.Empty) throw new ArgumentException("UnitId cannot be empty.");
        if (conversionRate <= 0) throw new ArgumentException("ConversionRate must be greater than 0.");
        if (saleVatRate < 0 || saleVatRate > 100) throw new ArgumentException("SaleVatRate must be 0..100");

        return new ProductUnitPrice
        {
            Id = id,
            ProductId = productId,
            UnitId = unitId,
            ConversionRate = conversionRate,
            Barcode = string.IsNullOrWhiteSpace(barcode) ? null : barcode.Trim(),
            SaleUnitPrice = saleUnitPrice,
            SaleUnitPriceCurrency = saleUnitPriceCurrency,
            SaleUnitPriceVatInclude = saleUnitPriceVatInclude,
            SaleVatRate = saleVatRate,
            IsBaseUnit = isBaseUnit,
            IsActive = true
        };
    }

    public void Update(
        decimal conversionRate,
        string? barcode,
        decimal saleUnitPrice,
        int saleUnitPriceCurrency,
        bool saleUnitPriceVatInclude,
        float saleVatRate,
        bool isBaseUnit)
    {
        if (conversionRate <= 0) throw new ArgumentException("ConversionRate must be greater than 0.");
        if (saleVatRate < 0 || saleVatRate > 100) throw new ArgumentException("SaleVatRate must be 0..100");

        ConversionRate = conversionRate;
        Barcode = string.IsNullOrWhiteSpace(barcode) ? null : barcode.Trim();
        SaleUnitPrice = saleUnitPrice;
        SaleUnitPriceCurrency = saleUnitPriceCurrency;
        SaleUnitPriceVatInclude = saleUnitPriceVatInclude;
        SaleVatRate = saleVatRate;
        IsBaseUnit = isBaseUnit;
    }

    public void SetActive(bool active) => IsActive = active;
}
