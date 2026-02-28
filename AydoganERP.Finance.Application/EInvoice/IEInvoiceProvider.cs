namespace AydoganERP.Finance.Application.EInvoice;

/// <summary>
/// E-Fatura entegratör sağlayıcı arayüzü.
/// Farklı entegratörler (Foriba, Logo, Edata vb.) bu arayüzü implemente eder.
/// </summary>
public interface IEInvoiceProvider
{
    /// <summary>Entegratör adı</summary>
    string ProviderName { get; }

    /// <summary>
    /// E-Fatura gönderir
    /// </summary>
    Task<EInvoiceSendResult> SendInvoiceAsync(EInvoiceRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// E-Fatura durumunu sorgular
    /// </summary>
    Task<EInvoiceStatusResult> GetStatusAsync(string eInvoiceUUID, CancellationToken cancellationToken = default);

    /// <summary>
    /// E-Fatura PDF'ini indirir
    /// </summary>
    Task<byte[]?> DownloadPdfAsync(string eInvoiceUUID, CancellationToken cancellationToken = default);

    /// <summary>
    /// E-Fatura iptal eder
    /// </summary>
    Task<EInvoiceCancelResult> CancelInvoiceAsync(string eInvoiceUUID, string reason, CancellationToken cancellationToken = default);
}

/// <summary>
/// E-Fatura gönderim isteği
/// </summary>
public class EInvoiceRequest
{
    public Guid InvoiceId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }

    // Fatura tipi
    public string InvoiceTypeCode { get; set; } = "SATIS"; // SATIS, IADE, ISTISNA vb.

    // Alıcı bilgileri
    public string CustomerTaxNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerAddress { get; set; } = string.Empty;
    public string CustomerCity { get; set; } = string.Empty;
    public string CustomerCountry { get; set; } = "Türkiye";

    // Satıcı bilgileri (şirket)
    public string SellerTaxNumber { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public string SellerAddress { get; set; } = string.Empty;
    public string SellerCity { get; set; } = string.Empty;

    // Tutar bilgileri
    public decimal SubTotal { get; set; }
    public decimal VatTotal { get; set; }
    public decimal DiscountTotal { get; set; }
    public decimal GrandTotal { get; set; }
    public string CurrencyCode { get; set; } = "TRY";

    // Fatura satırları
    public List<EInvoiceLineRequest> Lines { get; set; } = new();

    // Notlar
    public string? Notes { get; set; }
}

/// <summary>
/// E-Fatura satır bilgisi
/// </summary>
public class EInvoiceLineRequest
{
    public int LineNumber { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string UnitCode { get; set; } = "C62"; // Adet
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; }
    public decimal VatAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal LineTotal { get; set; }
}

/// <summary>
/// E-Fatura gönderim sonucu
/// </summary>
public class EInvoiceSendResult
{
    public bool Success { get; set; }
    public string? EInvoiceUUID { get; set; }
    public string? UblXml { get; set; }
    public string? ProviderResponse { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorCode { get; set; }
}

/// <summary>
/// E-Fatura durum sorgulama sonucu
/// </summary>
public class EInvoiceStatusResult
{
    public bool Success { get; set; }
    public string? Status { get; set; } // ACCEPTED, REJECTED, PENDING
    public string? ProviderResponse { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? ResponseDate { get; set; }
}

/// <summary>
/// E-Fatura iptal sonucu
/// </summary>
public class EInvoiceCancelResult
{
    public bool Success { get; set; }
    public string? ProviderResponse { get; set; }
    public string? ErrorMessage { get; set; }
}
