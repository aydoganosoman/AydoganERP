using AydoganERP.EInvoice.Abstractions.Enums;

namespace AydoganERP.EInvoice.Abstractions.Models;

/// <summary>
/// E-Fatura gönderim isteği
/// </summary>
public class EInvoiceRequest
{
    #region Belge Bilgileri

    /// <summary>ERP sistemindeki fatura ID</summary>
    public string InvoiceId { get; set; } = default!;

    /// <summary>Belge türü</summary>
    public EInvoiceDocumentType DocumentType { get; set; }

    /// <summary>Fatura senaryosu</summary>
    public EInvoiceScenario Scenario { get; set; }

    /// <summary>Fatura tipi</summary>
    public EInvoiceType InvoiceType { get; set; }

    /// <summary>ETTN (Elektronik Takip Numarası) - boş ise entegratör oluşturur</summary>
    public string? ETTN { get; set; }

    /// <summary>Fatura seri/sıra no öneki (ABC)</summary>
    public string? Prefix { get; set; }

    /// <summary>Fatura numarası</summary>
    public string InvoiceNumber { get; set; } = default!;

    /// <summary>Fatura tarihi</summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>Fatura saati</summary>
    public TimeSpan InvoiceTime { get; set; }

    #endregion

    #region Para Birimi

    /// <summary>Para birimi</summary>
    public CurrencyType Currency { get; set; } = CurrencyType.TRY;

    /// <summary>Döviz kuru</summary>
    public decimal ExchangeRate { get; set; } = 1;

    #endregion

    #region Gönderici Bilgileri (Firma)

    /// <summary>Firma vergi numarası</summary>
    public string SenderTaxNumber { get; set; } = default!;

    /// <summary>Firma unvanı</summary>
    public string SenderTitle { get; set; } = default!;

    /// <summary>Firma vergi dairesi</summary>
    public string? SenderTaxOffice { get; set; }

    /// <summary>Gönderici PK etiketi</summary>
    public string? SenderPkAlias { get; set; }

    /// <summary>Gönderici GB etiketi</summary>
    public string? SenderGbAlias { get; set; }
    
    /// <summary>Gönderici Şehir etiketi</summary>
    public string? SenderCity { get; set; }
    
    /// <summary>Gönderici İlçe etiketi</summary>
    public string? SenderDistrict { get; set; }
    
    /// <summary>Gönderici Adres etiketi</summary>
    public string? SenderAddress { get; set; }

    /// <summary>Gönderici TAPDK</summary>
    public string SenderTAPDK { get; set; } = default!;
    #endregion

    #region Alıcı Bilgileri (Müşteri)

    /// <summary>Müşteri vergi numarası / TC Kimlik No</summary>
    public string ReceiverTaxNumber { get; set; } = default!;

    /// <summary>Müşteri unvanı</summary>
    public string ReceiverTitle { get; set; } = default!;

    /// <summary>Müşteri vergi dairesi</summary>
    public string? ReceiverTaxOffice { get; set; }

    /// <summary>Müşteri il</summary>
    public string? ReceiverCity { get; set; }

    /// <summary>Müşteri ilçe</summary>
    public string? ReceiverDistrict { get; set; }

    /// <summary>Müşteri adres</summary>
    public string? ReceiverAddress { get; set; }

    /// <summary>Müşteri e-posta</summary>
    public string? ReceiverEmail { get; set; }

    /// <summary>Müşteri telefon</summary>
    public string? ReceiverPhone { get; set; }

    /// <summary>Alıcı PK etiketi</summary>
    public string? ReceiverPkAlias { get; set; }

    /// <summary>Alıcı GB etiketi</summary>
    public string? ReceiverGbAlias { get; set; }

    /// <summary>Müşteri TAPDK</summary>
    public string ReceiverTAPDK { get; set; } = default!;
    #endregion

    #region Tutar Bilgileri

    /// <summary>Satır toplamı</summary>
    public decimal SubTotal { get; set; }

    /// <summary>Toplam iskonto</summary>
    public decimal DiscountTotal { get; set; }

    /// <summary>KDV matrahı</summary>
    public decimal TaxableAmount { get; set; }

    /// <summary>KDV toplamı</summary>
    public decimal VatTotal { get; set; }

    /// <summary>Genel toplam (KDV dahil)</summary>
    public decimal GrandTotal { get; set; }

    /// <summary>Ödenecek tutar</summary>
    public decimal PayableAmount { get; set; }

    #endregion

    #region İade Fatura Bilgileri

    /// <summary>İade edilen fatura numarası</summary>
    public string? ReturnInvoiceNumber { get; set; }

    /// <summary>İade edilen fatura tarihi</summary>
    public DateTime? ReturnInvoiceDate { get; set; }

    #endregion

    #region Sipariş/İrsaliye Bilgileri

    /// <summary>Sipariş numarası</summary>
    public string? OrderNumber { get; set; }

    /// <summary>Sipariş tarihi</summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>İrsaliye numarası</summary>
    public string? WaybillNumber { get; set; }

    /// <summary>İrsaliye tarihi</summary>
    public DateTime? WaybillDate { get; set; }

    #endregion

    #region Banka Bilgileri (Kamu faturası için)

    /// <summary>Banka adı</summary>
    public string? BankName { get; set; }

    /// <summary>IBAN</summary>
    public string? BankIBAN { get; set; }

    /// <summary>Şube</summary>
    public string? BankBranch { get; set; }

    #endregion

    #region Diğer

    /// <summary>Fatura açıklaması</summary>
    public string? Description { get; set; }

    /// <summary>Notlar</summary>
    public List<string> Notes { get; set; } = new();

    /// <summary>Fatura satırları</summary>
    public List<EInvoiceRequestLine> Lines { get; set; } = new();

    #endregion
}
