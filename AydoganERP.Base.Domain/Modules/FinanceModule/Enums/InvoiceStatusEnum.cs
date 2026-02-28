namespace AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

public static class InvoiceStatusEnum
{
    public const int Draft = 0;             // Taslak
    public const int Approved = 1;          // Onaylandı
    public const int Cancelled = 2;         // İptal Edildi
    public const int EInvoiceSent = 3;      // E-Fatura Gönderildi
    public const int EInvoiceAccepted = 4;  // E-Fatura Kabul Edildi
    public const int EInvoiceRejected = 5;  // E-Fatura Reddedildi
}
