namespace AydoganERP.Base.Domain.Modules.CompanyModule.Enums;

public static class CurrencyEnum
{
    public const int TRY = 0;
    public const int USD = 1;
    public const int EUR = 2;
    public const int GBP = 3;

    public static string GetName(int currency) => currency switch
    {
        TRY => "TRY",
        USD => "USD",
        EUR => "EUR",
        GBP => "GBP",
        _ => "TRY"
    };

    public static Dictionary<int, string> GetAll() => new()
    {
        { TRY, "TRY - Türk Lirası" },
        { USD, "USD - Amerikan Doları" },
        { EUR, "EUR - Euro" },
        { GBP, "GBP - İngiliz Sterlini" }
    };
}
