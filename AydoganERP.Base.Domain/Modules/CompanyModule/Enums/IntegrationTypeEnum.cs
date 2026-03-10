namespace AydoganERP.Base.Domain.Modules.CompanyModule.Enums;

public static class IntegrationTypeEnum
{
    public const int Trendyol = 0;
    public const int TrendyolYemek = 1;
    public const int N11 = 2;
    public const int Hepsiburada = 3;
    public const int Shopier = 4;

    public static string GetName(int type) => type switch
    {
        Trendyol => "Trendyol",
        TrendyolYemek => "Trendyol Yemek",
        N11 => "N11",
        Hepsiburada => "Hepsiburada",
        Shopier => "Shopier",
        _ => "Bilinmiyor"
    };

    public static Dictionary<int, string> GetAll() => new()
    {
        { Trendyol, "Trendyol" },
        { TrendyolYemek, "Trendyol Yemek" },
        { N11, "N11" },
        { Hepsiburada, "Hepsiburada" },
        { Shopier, "Shopier" }
    };
}
