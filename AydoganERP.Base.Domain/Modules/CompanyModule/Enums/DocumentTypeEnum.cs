namespace AydoganERP.Base.Domain.Modules.CompanyModule.Enums;

public static class DocumentTypeEnum
{
    public const int EFatura = 0;
    public const int EArsiv = 1;
    public const int EIrsaliye = 2;
    public const int EMustahsil = 3;
    public const int ESerbest = 4;

    public static string GetName(int type) => type switch
    {
        EFatura => "E-Fatura",
        EArsiv => "E-Arşiv Fatura",
        EIrsaliye => "E-İrsaliye",
        EMustahsil => "E-Müstahsil",
        ESerbest => "E-Serbest Meslek",
        _ => "Bilinmiyor"
    };

    public static Dictionary<int, string> GetAll() => new()
    {
        { EFatura, "E-Fatura" },
        { EArsiv, "E-Arşiv Fatura" },
        { EIrsaliye, "E-İrsaliye" },
        { EMustahsil, "E-Müstahsil" },
        { ESerbest, "E-Serbest Meslek" }
    };
}
