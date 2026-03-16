namespace AydoganERP.EInvoice.Abstractions.Models;

/// <summary>
/// GİB mükellef hesap bilgisi
/// </summary>
public class GibAccount
{
    /// <summary>Vergi numarası / TC Kimlik No</summary>
    public string TaxNumber { get; set; } = default!;

    /// <summary>Firma/Kişi unvanı</summary>
    public string Title { get; set; } = default!;

    /// <summary>E-Fatura mükellefi mi?</summary>
    public bool IsEInvoiceUser { get; set; }

    /// <summary>E-Arşiv mükellefi mi?</summary>
    public bool IsEArchiveUser { get; set; }

    /// <summary>E-İrsaliye mükellefi mi?</summary>
    public bool IsEWaybillUser { get; set; }

    /// <summary>Posta kutusu (PK) etiketi</summary>
    public string? PkAlias { get; set; }

    /// <summary>Gönderici birim (GB) etiketi</summary>
    public string? GbAlias { get; set; }

    /// <summary>Kayıt tarihi</summary>
    public DateTime? RegisterDate { get; set; }

    /// <summary>Tüm PK etiketleri</summary>
    public List<string> PkAliases { get; set; } = new();

    /// <summary>Tüm GB etiketleri</summary>
    public List<string> GbAliases { get; set; } = new();
}
