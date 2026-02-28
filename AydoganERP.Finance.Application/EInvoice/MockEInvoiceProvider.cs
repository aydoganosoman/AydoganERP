using Microsoft.Extensions.Logging;

namespace AydoganERP.Finance.Application.EInvoice;

/// <summary>
/// Test amaçlı Mock E-Fatura Provider.
/// Gerçek entegrasyon için Foriba, Logo, Edata vb. implementasyonlar oluşturulabilir.
/// </summary>
public class MockEInvoiceProvider : IEInvoiceProvider
{
    private readonly ILogger<MockEInvoiceProvider> _logger;

    public string ProviderName => "Mock";

    public MockEInvoiceProvider(ILogger<MockEInvoiceProvider> logger)
    {
        _logger = logger;
    }

    public Task<EInvoiceSendResult> SendInvoiceAsync(EInvoiceRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Mock E-Fatura gönderimi: {InvoiceNumber}", request.InvoiceNumber);

        // Test için UUID oluştur
        var uuid = Guid.NewGuid().ToString();

        // Basit UBL XML oluştur
        var ublXml = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<Invoice xmlns=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"">
    <ID>{request.InvoiceNumber}</ID>
    <UUID>{uuid}</UUID>
    <IssueDate>{request.InvoiceDate:yyyy-MM-dd}</IssueDate>
</Invoice>";

        return Task.FromResult(new EInvoiceSendResult
        {
            Success = true,
            EInvoiceUUID = uuid,
            UblXml = ublXml,
            ProviderResponse = "Mock: E-Fatura başarıyla gönderildi"
        });
    }

    public Task<EInvoiceStatusResult> GetStatusAsync(string eInvoiceUUID, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Mock E-Fatura durum sorgusu: {UUID}", eInvoiceUUID);

        return Task.FromResult(new EInvoiceStatusResult
        {
            Success = true,
            Status = "ACCEPTED",
            ProviderResponse = "Mock: E-Fatura kabul edildi",
            ResponseDate = DateTime.UtcNow
        });
    }

    public Task<byte[]?> DownloadPdfAsync(string eInvoiceUUID, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Mock E-Fatura PDF indirme: {UUID}", eInvoiceUUID);

        // Boş PDF döndür (test için)
        return Task.FromResult<byte[]?>(null);
    }

    public Task<EInvoiceCancelResult> CancelInvoiceAsync(string eInvoiceUUID, string reason, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Mock E-Fatura iptal: {UUID} - Sebep: {Reason}", eInvoiceUUID, reason);

        return Task.FromResult(new EInvoiceCancelResult
        {
            Success = true,
            ProviderResponse = "Mock: E-Fatura başarıyla iptal edildi"
        });
    }
}
