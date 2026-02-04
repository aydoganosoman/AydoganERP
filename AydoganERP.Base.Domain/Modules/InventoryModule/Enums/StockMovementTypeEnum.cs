namespace AydoganERP.Base.Domain.Modules.InventoryModule.Enums;

public static class StockMovementTypeEnum
{
    public const int Opening = 1;
    public const int PurchaseIn = 2;
    public const int SaleOut = 3;
    public const int Adjustment = 4;

    public static string GetName(int value)
    {
        return value switch
        {
            StockMovementTypeEnum.Opening => "Opening",
            StockMovementTypeEnum.PurchaseIn => "PurchaseIn",
            StockMovementTypeEnum.SaleOut => "SaleOut",
            StockMovementTypeEnum.Adjustment => "Adjustment",
            _ => "No case availabe"
        };
    }
}