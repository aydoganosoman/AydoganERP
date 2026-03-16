namespace AydoganERP.EInvoice.Abstractions.Enums;

/// <summary>
/// E-Belge türü
/// </summary>
public enum EInvoiceDocumentType
{
    /// <summary>E-Fatura</summary>
    EInvoice = 0,

    /// <summary>E-Arşiv Fatura</summary>
    EArchive = 1,

    /// <summary>E-İrsaliye</summary>
    EWaybill = 2,

    /// <summary>E-Müstahsil Makbuzu</summary>
    EProducerReceipt = 3,

    /// <summary>E-Serbest Meslek Makbuzu</summary>
    ESelfEmploymentReceipt = 4
}
