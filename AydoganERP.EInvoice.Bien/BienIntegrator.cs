using AydoganERP.EInvoice.Abstractions.Enums;
using AydoganERP.EInvoice.Abstractions.Interfaces;
using AydoganERP.EInvoice.Abstractions.Models;
using AydoganERP.EInvoice.Abstractions.Models.Settings;
using AydoganERP.EInvoice.Bien.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AydoganERP.EInvoice.Bien;

/// <summary>
/// Bien E-Fatura Entegratör implementasyonu - SOAP entegrasyonu
/// </summary>
public class BienIntegrator : IEInvoiceIntegrator
{
    private readonly BienSettings _settings;
    private readonly ILogger<BienIntegrator> _logger;
    private readonly BienInvoiceService _invoiceService;

    public BienIntegrator(string settingsJson, ILoggerFactory loggerFactory)
    {
        _settings = JsonConvert.DeserializeObject<BienSettings>(settingsJson)
            ?? throw new ArgumentException("Geçersiz ayarlar", nameof(settingsJson));

        _logger = loggerFactory.CreateLogger<BienIntegrator>();

        // Token servisi ve fatura servisi oluştur
        var tokenService = new BienTokenService(_settings, loggerFactory.CreateLogger<BienTokenService>());
        _invoiceService = new BienInvoiceService(_settings, tokenService, loggerFactory.CreateLogger<BienInvoiceService>());
    }

    #region GİB Sorguları

    public async Task<GibAccount?> GetGibAccountAsync(string taxNumber)
    {
        try
        {
            return await _invoiceService.GetGibAccountAsync(taxNumber);
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
            return await _invoiceService.SendInvoiceAsync(request);
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
            return await _invoiceService.GetIncomingInvoicesAsync(startDate, endDate, pkAlias);
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
            return await _invoiceService.GetInvoiceStatusAsync(uuid, isOutbox);
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
