using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Entities;

/// <summary>
/// E-Fatura işlem log entity'si
/// </summary>
public class EInvoiceLog : AuditableEntity
{
    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }

    /// <summary>E-Fatura UUID (GIB tarafından atanan)</summary>
    public string? EInvoiceUUID { get; private set; }

    /// <summary>İşlem durumu</summary>
    public int Status { get; private set; }

    /// <summary>Gönderim tarihi</summary>
    public DateTime? SentAt { get; private set; }

    /// <summary>Yanıt tarihi</summary>
    public DateTime? ResponseAt { get; private set; }

    /// <summary>Entegratör isteği</summary>
    public string? ProviderRequest { get; private set; }
    
    /// <summary>Entegratör yanıtı</summary>
    public string? ProviderResponse { get; private set; }

    /// <summary>Hata mesajı</summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>Kullanılan entegratör</summary>
    public string? ProviderName { get; private set; }

    /// <summary>UBL XML içeriği</summary>
    public string? UblXmlContent { get; private set; }

    /// <summary>PDF dosya yolu</summary>
    public string? PdfPath { get; private set; }

    // Navigation
    public Invoice Invoice { get; private set; } = null!;

    private EInvoiceLog() { }

    public static EInvoiceLog Create(
        Guid id,
        Guid invoiceId,
        string providerName)
    {
        return new EInvoiceLog
        {
            Id = id,
            InvoiceId = invoiceId,
            ProviderName = providerName,
            Status = EInvoiceStatusEnum.Draft
        };
    }

    public void SetRequest(string providerRequest) => ProviderRequest = providerRequest;
    
    public void MarkAsSending(string ublXml)
    {
        Status = EInvoiceStatusEnum.Sending;
        SentAt = DateTime.UtcNow;
        UblXmlContent = ublXml;
    }

    public void MarkAsSent(string eInvoiceUUID, string providerResponse)
    {
        Status = EInvoiceStatusEnum.Sent;
        EInvoiceUUID = eInvoiceUUID;
        ProviderResponse = providerResponse;
    }

    public void MarkAsAccepted(string providerResponse, string? pdfPath = null)
    {
        Status = EInvoiceStatusEnum.Accepted;
        ResponseAt = DateTime.UtcNow;
        ProviderResponse = providerResponse;
        PdfPath = pdfPath;
    }

    public void MarkAsRejected(string providerResponse, string errorMessage)
    {
        Status = EInvoiceStatusEnum.Rejected;
        ResponseAt = DateTime.UtcNow;
        ProviderResponse = providerResponse;
        ErrorMessage = errorMessage;
    }

    public void MarkAsError(string errorMessage)
    {
        Status = EInvoiceStatusEnum.Error;
        ErrorMessage = errorMessage;
    }
}
