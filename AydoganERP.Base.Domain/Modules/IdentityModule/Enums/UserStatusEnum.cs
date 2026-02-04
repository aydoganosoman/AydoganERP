namespace AydoganERP.Base.Domain.Modules.IdentityModule.Enums;

public static class UserStatusEnum
{
    public const int Active = 1;
    public const int Passive = 2;
    public const int Locked = 3;

    public static string GetName(int value)
    {
        return value switch
        {
            UserStatusEnum.Active => "Active",
            UserStatusEnum.Passive => "Passive",
            UserStatusEnum.Locked => "Locked",
            _ => "No case availabe"
        };
    }
}