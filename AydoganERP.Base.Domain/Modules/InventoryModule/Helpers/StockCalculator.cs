using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Helpers;

public static class StockCalculator
{
    public static decimal CalculateOnHand(IEnumerable<StockMovement> movements)
        => movements.Sum(x => x.QuantityDelta);
}
