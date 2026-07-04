namespace AydoganERP.Base.Domain.Modules.CompanyModule.Enums;

public static class DocumentTypeEnum
{
    public const int EInvoice = 0;
    public const int EArchive = 1;
    public const int EWaybill = 2;
    public const int EProducer = 3;
    public const int ESelfEmployment = 4;

    public static string GetName(int type) => type switch
    {
        EInvoice => "E-Fatura",
        EArchive => "E-Arşiv Fatura",
        EWaybill => "E-İrsaliye",
        EProducer => "E-Müstahsil",
        ESelfEmployment => "E-Serbest Meslek",
        _ => "Bilinmiyor"
    };

    public static Dictionary<int, string> GetAll() => new()
    {
        { EInvoice, "E-Fatura" },
        { EArchive, "E-Arşiv Fatura" },
        { EWaybill, "E-İrsaliye" },
        { EProducer, "E-Müstahsil" },
        { ESelfEmployment, "E-Serbest Meslek" }
    };
}