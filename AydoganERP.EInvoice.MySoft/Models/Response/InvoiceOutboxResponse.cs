namespace AydoganERP.EInvoice.MySoft.Models.Response;

/// <summary>
/// MySoft fatura gönderim yanıtı
/// </summary>
public class InvoiceOutboxResponse
{
    public string InvoiceId { get; set; }
    public string InvoiceETTN { get; set; }
    public string DocNo { get; set; }
}

/// <summary>
/// Fatura durum yanıtı
/// </summary>
public class InvoiceStatusResponse
{
    public int Id { get; set; }
    public string? InvoiceETTN { get; set; }
    public string? DocNo { get; set; }
    public string? InvoiceStatusText { get; set; }
    public string? DeclineReason { get; set; }
    public string? EnvelopeIdentifier { get; set; }
    public string? GibEnvelopeStatusCode { get; set; }
    public string? EnvelopeStatusText { get; set; }
    public int EDocumentType { get; set; }
    public string? GTBRefNo { get; set; }
    public string? GTBGCBRegistryNo { get; set; }
    public string? GTBActualExportDate { get; set; }
    public int TryCount { get; set; }
}

/// <summary>
/// Gelen fatura yanıtı
/// </summary>
public class IncomingInvoiceResponse
{
    public int Id { get; set; }
    public string? Ettn { get; set; }
    public string? DocNo { get; set; }
    public string? DocDate { get; set; }
    public int Profile { get; set; }
    public int InvoiceType { get; set; }
    public string? InvoiceStatusText { get; set; }
    public string? VknTckn { get; set; }
    public string? AccountName { get; set; }
    public string? PkAlias { get; set; }
    public string? GbAlias { get; set; }
    public decimal LineExtensionAmount { get; set; }
    public decimal TaxExclusiveAmount { get; set; }
    public decimal TaxInclusiveAmount { get; set; }
    public decimal PayableRoundingAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public decimal AllowanceTotalAmount { get; set; }
    public decimal TaxTotalTra { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal CurrencyRate { get; set; }
    public DateTime CreateDate { get; set; }
    public string? ReferanceKey { get; set; }
}
