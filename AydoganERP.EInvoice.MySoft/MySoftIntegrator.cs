using AydoganERP.EInvoice.Abstractions.Enums;
using AydoganERP.EInvoice.Abstractions.Interfaces;
using AydoganERP.EInvoice.Abstractions.Models;
using AydoganERP.EInvoice.Abstractions.Models.Settings;
using AydoganERP.EInvoice.MySoft.Mappers;
using AydoganERP.EInvoice.MySoft.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AydoganERP.EInvoice.MySoft;

/// <summary>
/// MySoft E-Fatura Entegratör implementasyonu
/// </summary>
public class MySoftIntegrator : IEInvoiceIntegrator
{
    private readonly MySoftSettings _settings;
    private readonly ILogger<MySoftIntegrator> _logger;
    private readonly Uri _baseUri;
    private readonly TokenService _tokenService;
    private readonly AuthService _authService;
    private readonly GibAccountService _gibAccountService;
    private readonly InvoiceService _invoiceService;

    public MySoftIntegrator(string settingsJson, ILoggerFactory loggerFactory)
    {
        _settings = JsonConvert.DeserializeObject<MySoftSettings>(settingsJson)
            ?? throw new ArgumentException("Geçersiz ayarlar", nameof(settingsJson));

        _logger = loggerFactory.CreateLogger<MySoftIntegrator>();
        _baseUri = new Uri(_settings.Url);

        _tokenService = new TokenService();
        _authService = new AuthService(_tokenService, _baseUri);
        _gibAccountService = new GibAccountService(_tokenService, _baseUri);
        _invoiceService = new InvoiceService(_tokenService, _baseUri, _settings);
    }

    private async Task EnsureAuthenticatedAsync()
    {
        if (!_tokenService.IsValid())
        {
            var success = await _authService.LoginAsync(_settings.UserName, _settings.Password);
            if (!success)
            {
                throw new InvalidOperationException("MySoft kimlik doğrulama başarısız");
            }
        }
    }

    #region GİB Sorguları

    public async Task<GibAccount?> GetGibAccountAsync(string taxNumber)
    {
        try
        {
            await EnsureAuthenticatedAsync();

            var result = await _gibAccountService.GetGibAccountAsync(taxNumber);

            if (result.Succeed && result.Data != null)
            {
                return MySoftMapper.ToGibAccount(result.Data);
            }

            _logger.LogWarning("GİB hesap sorgulanamadı: {TaxNumber}, Hata: {Error}", taxNumber, result.Message);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GİB hesap sorgulama hatası: {TaxNumber}", taxNumber);
            throw;
        }
    }

    #endregion

    #region Fatura Gönderimi

    public async Task<EInvoiceResponse> SendInvoiceAsync(EInvoiceRequest request)
    {
        try
        {
            await EnsureAuthenticatedAsync();

            var mySoftRequest = MySoftMapper.ToInvoiceOutboxRequest(request, _settings.ConnectorGuid);
            var result = await _invoiceService.SendInvoiceAsync(mySoftRequest);

            if (result.Succeed && result.Data != null)
            {
                return EInvoiceResponse.CreateSuccess(
                    result.Data.InvoiceETTN ?? string.Empty,
                    result.Data.DocNo ?? string.Empty,
                    JsonConvert.SerializeObject(result));
            }

            return EInvoiceResponse.CreateError(
                result.Message ?? "Fatura gönderimi başarısız",
                rawResponse: JsonConvert.SerializeObject(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fatura gönderim hatası: {InvoiceId}", request.InvoiceId);
            return EInvoiceResponse.CreateError(ex.Message);
        }
    }

    #endregion

    #region Gelen Faturalar

    public async Task<List<IncomingInvoice>> GetIncomingInvoicesAsync(
        DateTime startDate,
        DateTime endDate,
        string? pkAlias = null)
    {
        try
        {
            await EnsureAuthenticatedAsync();

            var result = await _invoiceService.GetIncomingInvoicesAsync(startDate, endDate, pkAlias);

            if (result.Succeed && result.Data != null)
            {
                return result.Data.Select(MySoftMapper.ToIncomingInvoice).ToList();
            }

            _logger.LogWarning("Gelen faturalar alınamadı: {Error}", result.Message);
            return new List<IncomingInvoice>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Gelen fatura listesi hatası");
            throw;
        }
    }

    #endregion

    #region PDF/XML İndirme

    public async Task<byte[]> GetInvoicePdfAsync(string uuid, bool isOutbox)
    {
        try
        {
            await EnsureAuthenticatedAsync();
            return await _invoiceService.GetInvoicePdfAsync(uuid, isOutbox);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PDF indirme hatası: {UUID}", uuid);
            throw;
        }
    }

    public async Task<string> GetInvoiceXmlAsync(string uuid, bool isOutbox)
    {
        try
        {
            await EnsureAuthenticatedAsync();
            return await _invoiceService.GetInvoiceXmlAsync(uuid, isOutbox);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "XML indirme hatası: {UUID}", uuid);
            throw;
        }
    }

    #endregion

    #region Durum Sorgulama

    public async Task<EInvoiceStatusResult> GetInvoiceStatusAsync(string uuid, bool isOutbox)
    {
        try
        {
            await EnsureAuthenticatedAsync();

            var result = await _invoiceService.GetInvoiceStatusAsync(uuid, isOutbox);

            if (result.Succeed && result.Data != null)
            {
                return MySoftMapper.ToStatusResult(result.Data);
            }

            return new EInvoiceStatusResult
            {
                Success = false,
                ErrorMessage = result.Message
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Durum sorgulama hatası: {UUID}", uuid);
            throw;
        }
    }

    #endregion

    #region Gelen Fatura İşlemleri

    public async Task<bool> AcceptInvoiceAsync(string uuid)
    {
        try
        {
            await EnsureAuthenticatedAsync();
            return await _invoiceService.AcceptInvoiceAsync(uuid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fatura kabul hatası: {UUID}", uuid);
            throw;
        }
    }

    public async Task<bool> RejectInvoiceAsync(string uuid, string reason)
    {
        try
        {
            await EnsureAuthenticatedAsync();
            return await _invoiceService.RejectInvoiceAsync(uuid, reason);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fatura red hatası: {UUID}", uuid);
            throw;
        }
    }

    public async Task<bool> ReceiptInvoiceAsync(string uuid)
    {
        try
        {
            await EnsureAuthenticatedAsync();
            return await _invoiceService.ReceiptInvoiceAsync(uuid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fatura alındı işaretleme hatası: {UUID}", uuid);
            throw;
        }
    }

    #endregion

    #region E-Arşiv İptal

    public async Task<bool> CancelEArchiveAsync(string uuid, DateTime date, string type, string reason)
    {
        try
        {
            await EnsureAuthenticatedAsync();
            return await _invoiceService.CancelEArchiveAsync(uuid, date, type, reason);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "E-Arşiv iptal hatası: {UUID}", uuid);
            throw;
        }
    }

    #endregion
}
