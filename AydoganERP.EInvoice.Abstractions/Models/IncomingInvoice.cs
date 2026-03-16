using AydoganERP.EInvoice.Abstractions.Enums;

namespace AydoganERP.EInvoice.Abstractions.Models;

/// <summary>
/// Gelen fatura modeli
/// </summary>
public class IncomingInvoice
{
    /// <summary>Entegratör tarafındaki ID</summary>
    public string IntegratorId { get; set; } = default!;

    /// <summary>ETTN</summary>
    public string ETTN { get; set; } = default!;

    /// <summary>Fatura numarası</summary>
    public string InvoiceNumber { get; set; } = default!;

    /// <summary>Fatura tarihi</summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>Belge türü</summary>
    public EInvoiceDocumentType DocumentType { get; set; }

    /// <summary>Fatura senaryosu</summary>
    public EInvoiceScenario Scenario { get; set; }

    /// <summary>Fatura tipi</summary>
    public EInvoiceType InvoiceType { get; set; }

    /// <summary>Fatura durumu</summary>
    public EInvoiceStatus Status { get; set; }

    #region Gönderici Bilgileri

    /// <summary>Gönderici vergi no</summary>
    public string SenderTaxNumber { get; set; } = default!;

    /// <summary>Gönderici unvan</summary>
    public string SenderTitle { get; set; } = default!;

    /// <summary>Gönderici PK etiketi</summary>
    public string? SenderPkAlias { get; set; }

    /// <summary>Gönderici GB etiketi</summary>
    public string? SenderGbAlias { get; set; }

    #endregion

    #region Tutar Bilgileri

    /// <summary>Para birimi</summary>
    public CurrencyType Currency { get; set; }

    /// <summary>Döviz kuru</summary>
    public decimal ExchangeRate { get; set; }

    /// <summary>Satır toplamı</summary>
    public decimal SubTotal { get; set; }

    /// <summary>KDV matrahı</summary>
    public decimal TaxableAmount { get; set; }

    /// <summary>KDV toplamı</summary>
    public decimal VatTotal { get; set; }

    /// <summary>Toplam iskonto</summary>
    public decimal DiscountTotal { get; set; }

    /// <summary>Genel toplam</summary>
    public decimal GrandTotal { get; set; }

    /// <summary>Ödenecek tutar</summary>
    public decimal PayableAmount { get; set; }

    #endregion

    /// <summary>Entegratöre geldiği tarih</summary>
    public DateTime ReceivedAt { get; set; }

    /// <summary>Referans anahtar</summary>
    public string? ReferenceKey { get; set; }
}
