namespace AydoganERP.Base.Domain.Modules.SharedModule.Enums;

/// <summary>
/// Grup kullanım alanları
/// </summary>
[Flags]
public enum UsageAreaEnum
{
    None = 0,
    IncomeCard = 1,       // Gelir Kartı
    ExpenseCard = 2,      // Gider Kartı
    CustomerCard = 4,     // Cari Kartı
    ProductCard = 8,      // Stok Kartı
    All = IncomeCard | ExpenseCard | CustomerCard | ProductCard
}

public static class UsageAreaEnumExtensions
{
    public static string GetName(UsageAreaEnum value)
    {
        return value switch
        {
            UsageAreaEnum.IncomeCard => "Gelir Kartı",
            UsageAreaEnum.ExpenseCard => "Gider Kartı",
            UsageAreaEnum.CustomerCard => "Cari Kartı",
            UsageAreaEnum.ProductCard => "Stok Kartı",
            _ => value.ToString()
        };
    }

    public static List<string> GetNames(UsageAreaEnum value)
    {
        var names = new List<string>();
        if (value.HasFlag(UsageAreaEnum.IncomeCard)) names.Add("Gelir Kartı");
        if (value.HasFlag(UsageAreaEnum.ExpenseCard)) names.Add("Gider Kartı");
        if (value.HasFlag(UsageAreaEnum.CustomerCard)) names.Add("Cari Kartı");
        if (value.HasFlag(UsageAreaEnum.ProductCard)) names.Add("Stok Kartı");
        return names;
    }
}
