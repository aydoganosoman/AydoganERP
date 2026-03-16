using EInvoiceModels = AydoganERP.EInvoice.Abstractions.Models;

namespace AydoganERP.Finance.Application.EInvoice;

/// <summary>
/// E-Fatura servis interface'i.
/// Fatura entity'si ile entegratör arasındaki köprüyü sağlar.
/// </summary>
public interface IEInvoiceService
{
    /// <summary>
    /// Faturayı e-fatura/e-arşiv olarak gönderir.
    /// </summary>
    /// <param name="invoiceId">Fatura ID</param>
    /// <returns>Gönderim sonucu</returns>
    Task<EInvoiceModels.EInvoiceResponse> SendInvoiceAsync(Guid invoiceId);

    /// <summary>
    /// E-faturanın güncel durumunu sorgular ve günceller.
    /// </summary>
    /// <param name="invoiceId">Fatura ID</param>
    /// <returns>Durum bilgisi</returns>
    Task<EInvoiceModels.EInvoiceStatusResult> GetStatusAsync(Guid invoiceId);

    /// <summary>
    /// E-faturanın PDF'ini indirir.
    /// </summary>
    /// <param name="invoiceId">Fatura ID</param>
    /// <returns>PDF byte array</returns>
    Task<byte[]> GetPdfAsync(Guid invoiceId);

    /// <summary>
    /// E-faturanın UBL XML içeriğini indirir.
    /// </summary>
    /// <param name="invoiceId">Fatura ID</param>
    /// <returns>UBL XML string</returns>
    Task<string> GetXmlAsync(Guid invoiceId);

    /// <summary>
    /// Vergi numarasına göre GİB mükellef bilgisini sorgular.
    /// </summary>
    /// <param name="companyId">Firma ID (entegratör ayarları için)</param>
    /// <param name="taxNumber">Vergi numarası</param>
    /// <returns>GİB hesap bilgisi</returns>
    Task<EInvoiceModels.GibAccount?> GetGibAccountAsync(Guid companyId, string taxNumber);

/// <summary>
/// Belirtilen tarih aralığındaki gelen faturaları senkronize eder.
/// </summary>
/// <param name="companyId">Firma ID</param>
/// <param name="startDate">Başlangıç tarihi</param>
/// <param name="endDate">Bitiş tarihi</param>
/// <returns>Senkronizasyon sonucu</returns>
Task<IncomingSyncResult> SyncIncomingInvoicesAsync(Guid companyId, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Gelen ticari faturayı kabul eder.
    /// </summary>
    /// <param name="invoiceId">Fatura ID</param>
    /// <returns>İşlem başarılı mı?</returns>
    Task<bool> AcceptIncomingInvoiceAsync(Guid invoiceId);

    /// <summary>
    /// Gelen ticari faturayı reddeder.
    /// </summary>
    /// <param name="invoiceId">Fatura ID</param>
    /// <param name="reason">Red nedeni</param>
    /// <returns>İşlem başarılı mı?</returns>
    Task<bool> RejectIncomingInvoiceAsync(Guid invoiceId, string reason);

    /// <summary>
    /// E-Arşiv faturasını iptal eder.
    /// </summary>
    /// <param name="invoiceId">Fatura ID</param>
    /// <param name="reason">İptal nedeni</param>
    /// <returns>İşlem başarılı mı?</returns>
    Task<bool> CancelEArchiveAsync(Guid invoiceId, string reason);
}

/// <summary>
/// Gelen fatura senkronizasyon sonucu
/// </summary>
public record IncomingSyncResult(
    int SyncedCount,
    int TotalCount,
    List<string> FailedInvoices,
    string? ErrorMessage);
