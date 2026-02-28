namespace AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

/// <summary>
/// E-Fatura işlem durumları
/// </summary>
public static class EInvoiceStatusEnum
{
    /// <summary>Taslak</summary>
    public const int Draft = 0;

    /// <summary>Gönderiliyor</summary>
    public const int Sending = 1;

    /// <summary>Gönderildi</summary>
    public const int Sent = 2;

    /// <summary>Kabul Edildi</summary>
    public const int Accepted = 3;

    /// <summary>Reddedildi</summary>
    public const int Rejected = 4;

    /// <summary>İptal Edildi</summary>
    public const int Cancelled = 5;

    /// <summary>Hata</summary>
    public const int Error = 6;
}
