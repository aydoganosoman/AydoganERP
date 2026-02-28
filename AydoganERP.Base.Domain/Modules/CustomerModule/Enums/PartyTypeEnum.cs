namespace AydoganERP.Base.Domain.Modules.CustomerModule.Enums;

public static class PartyTypeEnum
{
    public const int Individual = 1;
    public const int Entity = 2;

    public static string GetName(int value)
    {
        return value switch
        {
            PartyTypeEnum.Individual => "Individual",
            PartyTypeEnum.Entity => "Entity",
            _ => "No case availabe"
        };
    }
}