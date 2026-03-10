namespace AydoganERP.Base.Domain.Modules.CompanyModule.Enums;

public class IntegratorCompanyTypeEnum
{
    public const int Mysoft = 1;
    public const int Bien = 2;
    
    public static string GetName(int type) => type switch
    {
        Mysoft => "Mysoft",
        Bien => "Bien",
        _ => "Bilinmiyor"
    };

    public static Dictionary<int, string> GetAll() => new()
    {
        { Mysoft, "Mysoft" },
        { Bien, "Bien" },
    };
}