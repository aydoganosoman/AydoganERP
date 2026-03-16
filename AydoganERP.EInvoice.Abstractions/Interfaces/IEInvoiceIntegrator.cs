using AydoganERP.EInvoice.Abstractions.Models;

namespace AydoganERP.EInvoice.Abstractions.Interfaces;

/// <summary>
/// E-Fatura entegratör interface'i.
/// Tüm e-fatura sağlayıcıları bu interface'i implement etmelidir.
/// </summary>
public interface IEInvoiceIntegrator
{
    #region GİB Sorguları

    /// <summary>
    /// Vergi numarasına göre GİB mükellef bilgisini sorgular.
    /// E-Fatura/E-Arşiv/E-İrsaliye mükellefi mi, PK/GB etiketleri neler.
    /// </summary>
    /// <param name="taxNumber">Vergi numarası veya TC Kimlik No</param>
    /// <returns>GİB hesap bilgisi veya null</returns>
    Task<GibAccount?> GetGibAccountAsync(string taxNumber);

    #endregion

    #region Fatura Gönderimi

    /// <summary>
    /// E-Fatura/E-Arşiv faturası gönderir.
    /// </summary>
    /// <param name="request">Fatura gönderim isteği</param>
    /// <returns>Gönderim sonucu</returns>
    Task<EInvoiceResponse> SendInvoiceAsync(EInvoiceRequest request);

    #endregion

    #region Gelen Faturalar

    /// <summary>
    /// Belirtilen tarih aralığındaki gelen faturaları listeler.
    /// </summary>
    /// <param name="startDate">Başlangıç tarihi</param>
    /// <param name="endDate">Bitiş tarihi</param>
    /// <param name="pkAlias">PK etiketi (opsiyonel)</param>
    /// <returns>Gelen fatura listesi</returns>
    Task<List<IncomingInvoice>> GetIncomingInvoicesAsync(
        DateTime startDate,
        DateTime endDate,
        string? pkAlias = null);

    #endregion

    #region PDF/XML İndirme

    /// <summary>
    /// Faturanın PDF'ini indirir.
    /// </summary>
    /// <param name="uuid">ETTN</param>
    /// <param name="isOutbox">Giden fatura mı?</param>
    /// <returns>PDF byte array</returns>
    Task<byte[]> GetInvoicePdfAsync(string uuid, bool isOutbox);

    /// <summary>
    /// Faturanın UBL XML içeriğini indirir.
    /// </summary>
    /// <param name="uuid">ETTN</param>
    /// <param name="isOutbox">Giden fatura mı?</param>
    /// <returns>UBL XML string</returns>
    Task<string> GetInvoiceXmlAsync(string uuid, bool isOutbox);

    #endregion

    #region Durum Sorgulama

    /// <summary>
    /// Faturanın güncel durumunu sorgular.
    /// </summary>
    /// <param name="uuid">ETTN</param>
    /// <param name="isOutbox">Giden fatura mı?</param>
    /// <returns>Durum bilgisi</returns>
    Task<EInvoiceStatusResult> GetInvoiceStatusAsync(string uuid, bool isOutbox);

    #endregion

    #region Gelen Fatura İşlemleri

    /// <summary>
    /// Gelen ticari faturayı kabul eder.
    /// </summary>
    /// <param name="uuid">ETTN</param>
    /// <returns>İşlem başarılı mı?</returns>
    Task<bool> AcceptInvoiceAsync(string uuid);

    /// <summary>
    /// Gelen ticari faturayı reddeder.
    /// </summary>
    /// <param name="uuid">ETTN</param>
    /// <param name="reason">Red nedeni</param>
    /// <returns>İşlem başarılı mı?</returns>
    Task<bool> RejectInvoiceAsync(string uuid, string reason);

    /// <summary>
    /// Gelen faturayı alındı olarak işaretler.
    /// </summary>
    /// <param name="uuid">ETTN</param>
    /// <returns>İşlem başarılı mı?</returns>
    Task<bool> ReceiptInvoiceAsync(string uuid);

    #endregion

    #region E-Arşiv İptal

    /// <summary>
    /// E-Arşiv faturasını iptal eder.
    /// </summary>
    /// <param name="uuid">ETTN</param>
    /// <param name="date">İptal tarihi</param>
    /// <param name="type">İptal tipi</param>
    /// <param name="reason">İptal nedeni</param>
    /// <returns>İşlem başarılı mı?</returns>
    Task<bool> CancelEArchiveAsync(string uuid, DateTime date, string type, string reason);

    #endregion
}
