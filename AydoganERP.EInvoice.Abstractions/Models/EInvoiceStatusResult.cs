using AydoganERP.EInvoice.Abstractions.Enums;

namespace AydoganERP.EInvoice.Abstractions.Models;

/// <summary>
/// E-Fatura durum sorgulama sonucu
/// </summary>
public class EInvoiceStatusResult
{
    /// <summary>Sorgu başarılı mı?</summary>
    public bool Success { get; set; }

    /// <summary>ETTN</summary>
    public string? ETTN { get; set; }

    /// <summary>Fatura numarası</summary>
    public string? InvoiceNumber { get; set; }

    /// <summary>Fatura durumu</summary>
    public EInvoiceStatus Status { get; set; }

    /// <summary>Durum açıklaması</summary>
    public string? StatusDescription { get; set; }

    /// <summary>Ret/İptal nedeni</summary>
    public string? DeclineReason { get; set; }

    /// <summary>Zarf ID</summary>
    public string? EnvelopeId { get; set; }

    /// <summary>Zarf durumu</summary>
    public string? EnvelopeStatus { get; set; }

    /// <summary>GİB zarf durum kodu</summary>
    public string? GibEnvelopeStatusCode { get; set; }

    /// <summary>Deneme sayısı</summary>
    public int TryCount { get; set; }

    /// <summary>Hata mesajı</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Ham yanıt</summary>
    public string? RawResponse { get; set; }

    #region İhracat Faturası için GTB Bilgileri

    /// <summary>GTB Referans No</summary>
    public string? GTBRefNo { get; set; }

    /// <summary>GTB Tescil No</summary>
    public string? GTBRegistryNo { get; set; }

    /// <summary>GTB Fiili İhracat Tarihi</summary>
    public DateTime? GTBExportDate { get; set; }

    #endregion
}
