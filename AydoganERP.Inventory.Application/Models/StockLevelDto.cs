namespace AydoganERP.Inventory.Application.Models;

public class StockLevelDto
{
    public Guid ProductId { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal OnHand { get; set; }
    public string UnitName { get; set; } = string.Empty;
}
