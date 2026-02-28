using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Inventory.Application.Models;

public class ProductSerialNumberDto : IMapFrom<ProductSerialNumber>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public int Status { get; set; }
    
    // Alış bilgileri
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public Guid? PurchaseCustomerId { get; set; }
    public string? PurchaseCustomerName { get; set; }
    
    // Satış bilgileri
    public DateTime? SaleDate { get; set; }
    public decimal? SalePrice { get; set; }
    public Guid? SaleCustomerId { get; set; }
    public string? SaleCustomerName { get; set; }
    
    // Garanti
    public DateTime? WarrantyEndDate { get; set; }
    
    public string? Notes { get; set; }
}
