namespace AydoganERP.Base.Domain.Modules.CompanyModule.Enums;

public static class CompanyTypeEnum
{
    public const int Corporate = 0;  // Tüzel Kişi
    public const int Individual = 1; // Gerçek Kişi

    public static string GetName(int value)
    {
        return value switch
        {
            Corporate => "Tüzel Kişi",
            Individual => "Gerçek Kişi",
            _ => "Bilinmiyor"
        };
    }
}
