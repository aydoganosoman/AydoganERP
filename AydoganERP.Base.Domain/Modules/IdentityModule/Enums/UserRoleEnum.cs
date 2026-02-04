namespace AydoganERP.Base.Domain.Modules.IdentityModule.Enums;

public static class UserRoleEnum
{
    public const int SuperAdmin = 1;
    public const int CompanyAdmin = 2;
    public const int CustomerUser = 3;

    public static string GetName(int value)
    {
        return value switch
        {
            UserRoleEnum.SuperAdmin => "SuperAdmin",
            UserRoleEnum.CompanyAdmin => "CompanyAdmin",
            UserRoleEnum.CustomerUser => "CustomerUser",
            _ => "No case availabe"
        };
    }
}