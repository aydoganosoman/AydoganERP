namespace AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;

public sealed record TaxInfo(string? TaxNumber, string? TaxOffice)
{
    public static TaxInfo Empty => new(null, null);

    public bool IsValidForInvoice()
        => !string.IsNullOrWhiteSpace(TaxNumber) && !string.IsNullOrWhiteSpace(TaxOffice);
}
