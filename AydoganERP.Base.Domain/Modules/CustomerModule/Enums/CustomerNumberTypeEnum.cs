namespace AydoganERP.Base.Domain.Modules.CustomerModule.Enums;

public class CustomerNumberTypeEnum
{
    public const int TAPDK = 1;
    public const int Mersis = 2;
    public const int EPDK = 3;
    public const int CustomerNo = 4;

    public static string GetName(int value)
    {
        return value switch
        {
            CustomerNumberTypeEnum.TAPDK => "TAPDK",
            CustomerNumberTypeEnum.Mersis => "Mersis",
            CustomerNumberTypeEnum.EPDK => "EPDK",
            CustomerNumberTypeEnum.CustomerNo => "CustomerNo",
            _ => "No case availabe"
        };
    }
}