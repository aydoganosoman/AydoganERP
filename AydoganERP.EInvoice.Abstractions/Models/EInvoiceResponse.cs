using AydoganERP.EInvoice.Abstractions.Enums;

namespace AydoganERP.EInvoice.Abstractions.Models;

/// <summary>
/// E-Fatura gönderim yanıtı
/// </summary>
public class EInvoiceResponse
{
    /// <summary>İşlem başarılı mı?</summary>
    public bool Success { get; set; }

    /// <summary>ETTN (GİB tarafından atanan UUID)</summary>
    public string? ETTN { get; set; }

    /// <summary>Fatura numarası (entegratör tarafından atanan)</summary>
    public string? InvoiceNumber { get; set; }

    /// <summary>Belge durumu</summary>
    public EInvoiceStatus Status { get; set; }

    /// <summary>Entegratör mesajı</summary>
    public string? Message { get; set; }

    /// <summary>Hata mesajı</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Hata kodu</summary>
    public string? ErrorCode { get; set; }

    /// <summary>Entegratör ham yanıtı (JSON)</summary>
    public string? RawResponse { get; set; }

    /// <summary>Gönderim tarihi</summary>
    public DateTime? SentAt { get; set; }

    /// <summary>Referans anahtar (ERP fatura ID)</summary>
    public string? ReferenceKey { get; set; }

    public static EInvoiceResponse CreateSuccess(string ettn, string invoiceNumber, string? rawResponse = null)
    {
        return new EInvoiceResponse
        {
            Success = true,
            ETTN = ettn,
            InvoiceNumber = invoiceNumber,
            Status = EInvoiceStatus.Sent,
            SentAt = DateTime.UtcNow,
            RawResponse = rawResponse
        };
    }

    public static EInvoiceResponse CreateError(string errorMessage, string? errorCode = null, string? rawResponse = null)
    {
        return new EInvoiceResponse
        {
            Success = false,
            Status = EInvoiceStatus.Error,
            ErrorMessage = errorMessage,
            ErrorCode = errorCode,
            RawResponse = rawResponse
        };
    }
}
