using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Inventory.Application.Models;

public class ProductDto : IMapFrom<Product>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid UnitId { get; set; }
    public string? UnitName { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    
    // Alış Fiyat Bilgileri
    public decimal PurchaseUnitPrice { get; set; }
    public int PurchaseUnitPriceCurrency { get; set; }
    public bool PurchaseUnitPriceVatInclude { get; set; }
    public float PurchaseVatRate { get; set; }
    
    // Satış Fiyat Bilgileri
    public decimal SaleUnitPrice { get; set; }
    public int SaleUnitPriceCurrency { get; set; }
    public bool SaleUnitPriceVatInclude { get; set; }
    public float SaleVatRate { get; set; }
    
    public bool IsLotTracked { get; set; }
    public bool IsSerialTracked { get; set; }
    public bool IsActive { get; set; }
    public List<ProductBarcodeDto> Barcodes { get; set; } = new();
    public List<ProductSupplierDto> Suppliers { get; set; } = new();
    public List<ProductSerialNumberDto> SerialNumbers { get; set; } = new();
}
