using AydoganERP.EInvoice.Abstractions.Enums;

namespace AydoganERP.EInvoice.Bien.Services;

public class EnumService
{
    public static string GetProfileType(EInvoiceScenario invoiceScenarioType)
    {
        return invoiceScenarioType switch
        {
            EInvoiceScenario.Basic => "TEMELFATURA",
            EInvoiceScenario.Commercial => "TICARIFATURA",
            EInvoiceScenario.PassengerAccompanied => "YOLCUBERABERFATURA",
            EInvoiceScenario.Export => "IHRACAT",
            EInvoiceScenario.Public => "KAMU",
            EInvoiceScenario.EArchive => "EARSIVFATURA"
        };
    }
    
    public static string GetInvoiceType(EInvoiceType invoiceType)
    {
        return invoiceType switch
        {
            EInvoiceType.Sales => "SATIS",
            EInvoiceType.Return => "IADE",
            EInvoiceType.Withholding => "TEVKIFAT",
            EInvoiceType.Exception => "ISTISNA",
            EInvoiceType.SpecialBase => "OZELMATRAH",
            EInvoiceType.SGK => "SGK"
        };

    }
}