namespace AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;

public sealed record ContactInfo(string? Email, string? Phone, string? Fax = null, string? Website = null)
{
    public static ContactInfo Empty => new(null, null, null, null);
}
