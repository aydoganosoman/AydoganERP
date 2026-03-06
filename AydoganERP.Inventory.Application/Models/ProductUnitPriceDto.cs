using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Inventory.Application.Models;

public class ProductUnitPriceDto : IMapFrom<ProductUnitPrice>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UnitId { get; set; }
    public string? UnitName { get; set; }
    
    /// <summary>
    /// Ana birime dönüşüm oranı (örn: 1 Koli = 12 Adet -> 12)
    /// </summary>
    public decimal ConversionRate { get; set; } = 1;
    
    public string? Barcode { get; set; }
    
    public decimal SaleUnitPrice { get; set; }
    public int SaleUnitPriceCurrency { get; set; }
    public bool SaleUnitPriceVatInclude { get; set; }
    public float SaleVatRate { get; set; }
    
    public bool IsBaseUnit { get; set; }
    public bool IsActive { get; set; } = true;
}
