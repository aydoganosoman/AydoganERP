namespace AydoganERP.Base.Domain.Modules.CompanyModule.Enums;

public static class CompanyStatusEnum
{
    public const int Active = 1;
    public const int Suspended = 2;
    public const int Passive = 3;

    public static string GetName(int value)
    {
        return value switch
        {
            CompanyStatusEnum.Active => "Active",
            CompanyStatusEnum.Suspended => "Suspended",
            CompanyStatusEnum.Passive => "Passive",
            _ => "No case availabe"
        };
    }
}