namespace AydoganERP.Base.Domain.Modules.CustomerModule.Enums;

public static class CustomerTypeEnum
{
    public const int Customer = 0;
    public const int Suplier = 1;
    public const int Both = 2;

    public static string GetName(int value)
    {
        return value switch
        {
            CustomerTypeEnum.Customer => "Customer",
            CustomerTypeEnum.Suplier => "Suplier",
            CustomerTypeEnum.Both => "Both",
            _ => "No case availabe"
        };
    }
}