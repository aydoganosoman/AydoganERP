namespace AydoganERP.Base.Domain.Modules.CustomerModule.Enums;

public static class CustomerStatusEnum
{
    public const int Active = 0;
    public const int Passive = 1;
    public const int Blocked = 2;

    public static string GetName(int value)
    {
        return value switch
        {
            CustomerStatusEnum.Active => "Active",
            CustomerStatusEnum.Passive => "Passive",
            CustomerStatusEnum.Blocked => "Blocked",
            _ => "No case availabe"
        };
    }
}