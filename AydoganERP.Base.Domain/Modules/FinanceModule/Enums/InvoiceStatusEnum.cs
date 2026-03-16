namespace AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

public static class InvoiceStatusEnum
{
    // Genel durumlar
    public const int Draft = 0;             // Taslak
    public const int Approved = 1;          // Onaylandı
    public const int Cancelled = 2;         // İptal Edildi

    // Giden fatura (E-Fatura) durumları
    public const int EInvoiceSent = 3;      // E-Fatura Gönderildi
    public const int EInvoiceAccepted = 4;  // E-Fatura Kabul Edildi (Alıcı tarafından)
    public const int EInvoiceRejected = 5;  // E-Fatura Reddedildi (Alıcı tarafından)

    // Gelen fatura durumları
    public const int Received = 6;          // Gelen Fatura Alındı (Entegratörden)
    public const int PendingApproval = 7;   // Kabul Bekleniyor (Ticari fatura için 8 gün)
    public const int AcceptedByUs = 8;      // Tarafımızca Kabul Edildi
    public const int RejectedByUs = 9;      // Tarafımızca Reddedildi
}
