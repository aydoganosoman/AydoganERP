namespace AydoganERP.EInvoice.Abstractions.Models;

/// <summary>
/// E-Fatura satır bilgisi
/// </summary>
public class EInvoiceRequestLine
{
    /// <summary>Satır no</summary>
    public int LineNumber { get; set; }

    /// <summary>Ürün/Hizmet kodu</summary>
    public string ProductCode { get; set; } = default!;

    /// <summary>Ürün/Hizmet adı</summary>
    public string ProductName { get; set; } = default!;

    /// <summary>Miktar</summary>
    public decimal Quantity { get; set; }

    /// <summary>Birim kodu (C62=Adet, KGM=Kilogram, MTR=Metre vb.)</summary>
    public string UnitCode { get; set; } = "C62";

    /// <summary>Birim fiyat</summary>
    public decimal UnitPrice { get; set; }

    /// <summary>Satır tutarı (Miktar x Birim Fiyat)</summary>
    public decimal LineTotal { get; set; }

    /// <summary>İskonto oranı (%)</summary>
    public decimal DiscountRate { get; set; }

    /// <summary>İskonto tutarı</summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>KDV oranı (%)</summary>
    public decimal VatRate { get; set; }

    /// <summary>KDV tutarı</summary>
    public decimal VatAmount { get; set; }

    /// <summary>KDV dahil toplam</summary>
    public decimal LineTotalWithVat { get; set; }

    /// <summary>Açıklama</summary>
    public string? Description { get; set; }

    /// <summary>KDV istisna kodu (301, 302 vb.)</summary>
    public string? VatExemptionCode { get; set; }

    /// <summary>KDV istisna açıklaması</summary>
    public string? VatExemptionReason { get; set; }

    /// <summary>Tevkifat kodu</summary>
    public string? WithholdingTaxCode { get; set; }

    /// <summary>Tevkifat adı</summary>
    public string? WithholdingTaxName { get; set; }

    /// <summary>Tevkifat oranı</summary>
    public decimal? WithholdingTaxRate { get; set; }

    /// <summary>Tevkifat tutarı</summary>
    public decimal? WithholdingTaxAmount { get; set; }
}
