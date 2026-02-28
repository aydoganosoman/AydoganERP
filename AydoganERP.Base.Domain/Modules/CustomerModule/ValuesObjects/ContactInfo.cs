namespace AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;

public sealed record ContactInfo(string? Email, string? Phone)
{
    public static ContactInfo Empty => new(null, null);
}
