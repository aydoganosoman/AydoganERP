using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Inventory.Application.Models;

public class StockBatchDto : IMapFrom<StockBatch>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
}
