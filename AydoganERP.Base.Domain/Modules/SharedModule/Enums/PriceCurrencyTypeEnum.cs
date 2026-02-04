namespace AydoganERP.Base.Domain.Modules.SharedModule.Enums;

public static class PriceCurrencyTypeEnum
{
    public const int TurkishLira = 1;
    public const int Euro = 2;
    public const int Dolar = 3;
    
    public static string GetName(int value)
    {
        return value switch
        {
            PriceCurrencyTypeEnum.TurkishLira => "TurkishLira",
            PriceCurrencyTypeEnum.Euro => "Euro",
            PriceCurrencyTypeEnum.Dolar => "Dolar",
            _ => "No case availabe"
        };
    }
}