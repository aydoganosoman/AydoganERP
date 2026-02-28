using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Inventory.Application.Models;

public class StockMovementDto : IMapFrom<StockMovement>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public DateOnly Date { get; set; }
    public int Type { get; set; }
    public string? TypeName { get; set; }
    public decimal QuantityDelta { get; set; }
    public string? ReferenceType { get; set; }
    public Guid? ReferenceId { get; set; }
    public string? ReferenceNo { get; set; }
    public string Description { get; set; } = string.Empty;
}
