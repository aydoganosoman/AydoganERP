namespace AydoganERP.EInvoice.Abstractions.Enums;

/// <summary>
/// E-Fatura durumu
/// </summary>
public enum EInvoiceStatus
{
    /// <summary>Taslak</summary>
    Draft = 0,

    /// <summary>Gönderiliyor</summary>
    Sending = 1,

    /// <summary>Gönderildi</summary>
    Sent = 2,

    /// <summary>Kabul Edildi</summary>
    Accepted = 3,

    /// <summary>Reddedildi</summary>
    Rejected = 4,

    /// <summary>İptal Edildi</summary>
    Cancelled = 5,

    /// <summary>Hata</summary>
    Error = 6,

    /// <summary>Beklemede (Ticari faturada)</summary>
    Pending = 7,

    /// <summary>Alındı (Gelen fatura için)</summary>
    Received = 8
}
