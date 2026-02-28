using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Inventory.Application.Models;

public class ProductBarcodeDto : IMapFrom<ProductBarcode>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = "Adet";
}
