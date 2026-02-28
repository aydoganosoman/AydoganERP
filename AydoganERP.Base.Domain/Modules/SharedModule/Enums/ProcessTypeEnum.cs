namespace AydoganERP.Base.Domain.Modules.SharedModule.Enums;

/// <summary>
/// Kategori için seçilebilecek süreçler
/// </summary>
[Flags]
public enum ProcessTypeEnum
{
    None = 0,
    PurchaseInvoice = 1,          // Fatura Alış
    SalesInvoice = 2,             // Fatura Satış
    PurchaseWaybill = 4,          // İrsaliye Alış
    SalesWaybill = 8,             // İrsaliye Satış
    SelfEmploymentReceipt = 16,   // Serbest Meslek Makbuzu
    ProducerReceipt = 32,         // Müstahsil Makbuzu
    IncomeCard = 64,              // Gelir Kartı
    ExpenseCard = 128,            // Gider Kartı
    CustomerCard = 256,           // Cari Kartı
    ProductCard = 512,            // Stok Kartı
    All = PurchaseInvoice | SalesInvoice | PurchaseWaybill | SalesWaybill | 
          SelfEmploymentReceipt | ProducerReceipt | IncomeCard | ExpenseCard | 
          CustomerCard | ProductCard
}

public static class ProcessTypeEnumExtensions
{
    public static string GetName(ProcessTypeEnum value)
    {
        return value switch
        {
            ProcessTypeEnum.PurchaseInvoice => "Fatura Alış",
            ProcessTypeEnum.SalesInvoice => "Fatura Satış",
            ProcessTypeEnum.PurchaseWaybill => "İrsaliye Alış",
            ProcessTypeEnum.SalesWaybill => "İrsaliye Satış",
            ProcessTypeEnum.SelfEmploymentReceipt => "Serbest Meslek Makbuzu",
            ProcessTypeEnum.ProducerReceipt => "Müstahsil Makbuzu",
            ProcessTypeEnum.IncomeCard => "Gelir Kartı",
            ProcessTypeEnum.ExpenseCard => "Gider Kartı",
            ProcessTypeEnum.CustomerCard => "Cari Kartı",
            ProcessTypeEnum.ProductCard => "Stok Kartı",
            _ => value.ToString()
        };
    }

    public static List<string> GetNames(ProcessTypeEnum value)
    {
        var names = new List<string>();
        if (value.HasFlag(ProcessTypeEnum.PurchaseInvoice)) names.Add("Fatura Alış");
        if (value.HasFlag(ProcessTypeEnum.SalesInvoice)) names.Add("Fatura Satış");
        if (value.HasFlag(ProcessTypeEnum.PurchaseWaybill)) names.Add("İrsaliye Alış");
        if (value.HasFlag(ProcessTypeEnum.SalesWaybill)) names.Add("İrsaliye Satış");
        if (value.HasFlag(ProcessTypeEnum.SelfEmploymentReceipt)) names.Add("Serbest Meslek Makbuzu");
        if (value.HasFlag(ProcessTypeEnum.ProducerReceipt)) names.Add("Müstahsil Makbuzu");
        if (value.HasFlag(ProcessTypeEnum.IncomeCard)) names.Add("Gelir Kartı");
        if (value.HasFlag(ProcessTypeEnum.ExpenseCard)) names.Add("Gider Kartı");
        if (value.HasFlag(ProcessTypeEnum.CustomerCard)) names.Add("Cari Kartı");
        if (value.HasFlag(ProcessTypeEnum.ProductCard)) names.Add("Stok Kartı");
        return names;
    }
}
