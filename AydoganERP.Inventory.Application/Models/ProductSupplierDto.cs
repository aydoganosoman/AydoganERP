using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Inventory.Application.Models;

public class ProductSupplierDto : IMapFrom<ProductSupplier>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
