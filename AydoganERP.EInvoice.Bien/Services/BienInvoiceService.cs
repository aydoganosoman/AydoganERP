using System.Text;
using AydoganERP.EInvoice.Abstractions.Enums;
using AydoganERP.EInvoice.Abstractions.Interfaces;
using AydoganERP.EInvoice.Abstractions.Models;
using AydoganERP.EInvoice.Abstractions.Models.Settings;
using AydoganERP.EInvoice.Bien.Integration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AydoganERP.EInvoice.Bien.Services;

/// <summary>
/// Bien fatura servisi - SOAP entegrasyonu
/// </summary>
public class BienInvoiceService : IEInvoiceIntegrator
{
    private readonly BienSettings _settings;
    private readonly BienTokenService _tokenService;
    private readonly ILogger<BienInvoiceService> _logger;

    public BienInvoiceService(
        BienSettings settings,
        BienTokenService tokenService,
        ILogger<BienInvoiceService> logger)
    {
        _settings = settings;
        _tokenService = tokenService;
        _logger = logger;
    }

    #region GİB Sorguları

    public async Task<GibAccount?> GetGibAccountAsync(string taxNumber)
    {
        _logger.LogDebug("Bien GİB mükellef sorgulama: {TaxNumber}", taxNumber);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var response = await client.TryToGetAddressFromVknTcknAsync(taxNumber, QueryType.Normal);

            if (!response.IsSucceded)
            {
                _logger.LogWarning("Bien GİB sorgulama başarısız: {Message}", response.Message);
                return null;
            }

            if (response.Value == null)
            {
                _logger.LogDebug("Bien GİB kaydı bulunamadı: {TaxNumber}", taxNumber);
                return null;
            }

            return BienMapper.ToGibAccount(response.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien GİB sorgulama hatası: {TaxNumber}", taxNumber);
            throw;
        }
    }

    #endregion

    #region Fatura Gönderimi

    public async Task<EInvoiceResponse> SendInvoiceAsync(EInvoiceRequest request)
    {
        _logger.LogDebug("Bien fatura gönderimi: {InvoiceId}", request.InvoiceId);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var invoiceInfo = CreateInvoiceInfo(request);
            var response = await client.SendInvoiceAsync(new[] { invoiceInfo });

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien fatura gönderim hatası: {Message}", response.Message);
                return EInvoiceResponse.CreateError(
                    response.Message ?? "Fatura gönderilemedi",
                    rawResponse: JsonConvert.SerializeObject(response));
            }

            var result = response.Value?.FirstOrDefault();
            if (result == null)
            {
                return EInvoiceResponse.CreateError("Fatura yanıtı alınamadı");
            }

            _logger.LogInformation("Bien fatura gönderildi: {ETTN}, Numara: {Number}",
                result.Id, result.Number);

            return new EInvoiceResponse
            {
                Success = true,
                ETTN = result.Id,
                InvoiceNumber = result.Number,
                Status = EInvoiceStatus.Sent,
                SentAt = DateTime.UtcNow,
                ReferenceKey = request.InvoiceId,
                RawResponse = JsonConvert.SerializeObject(response)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien fatura gönderim hatası: {InvoiceId}", request.InvoiceId);
            return EInvoiceResponse.CreateError(ex.Message);
        }
    }

    private InvoiceInfo CreateInvoiceInfo(EInvoiceRequest request)
    {
        // UBL XML oluşturma yerine basit bir fatura bilgisi
        // Gerçek implementasyonda UBL XML builder kullanılmalı
        var invoiceInfo = new InvoiceInfo
        {
            LocalDocumentId = request.InvoiceId,
            CreateDateUtc = DateTime.UtcNow,
            Scenario = BienMapper.ToInvoiceScenarioChoosen(request.Scenario, request.DocumentType),
            TargetCustomer = new CustomerInfo
            {
                VknTckn = request.ReceiverTaxNumber,
                Title = request.ReceiverTitle
            }
        };

        return invoiceInfo;
    }

    #endregion

    #region Gelen Faturalar

    public async Task<List<IncomingInvoice>> GetIncomingInvoicesAsync(
        DateTime startDate,
        DateTime endDate,
        string? pkAlias = null)
    {
        _logger.LogDebug("Bien gelen faturalar sorgulanıyor: {StartDate} - {EndDate}", startDate, endDate);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var query = new InboxInvoiceListQueryModel
            {
                ExecutionStartDate = startDate,
                ExecutionEndDate = endDate,
                OnlyNewestInvoices = true,
                PageIndex = 0,
                PageSize = 1000
            };

            var response = await client.GetInboxInvoiceListAsync(query);

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien gelen fatura sorgulama hatası: {Message}", response.Message);
                return new List<IncomingInvoice>();
            }

            var items = response.Value?.Items ?? Array.Empty<InboxInvoiceListItem>();
            _logger.LogDebug("Bien gelen fatura sayısı: {Count}", items.Length);

            return items.Select(BienMapper.ToIncomingInvoice).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien gelen fatura sorgulama hatası");
            throw;
        }
    }

    #endregion

    #region PDF/XML İndirme

    public async Task<byte[]> GetInvoicePdfAsync(string uuid, bool isOutbox)
    {
        _logger.LogDebug("Bien PDF indirme: {UUID}, Giden: {IsOutbox}", uuid, isOutbox);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            InvoiceDataResponse response;
            if (isOutbox)
            {
                response = await client.GetOutboxInvoicePdfAsync(uuid);
            }
            else
            {
                response = await client.GetInboxInvoicePdfAsync(uuid);
            }

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien PDF indirme hatası: {Message}", response.Message);
                throw new InvalidOperationException($"PDF indirilemedi: {response.Message}");
            }

            return response.Value?.Data ?? Array.Empty<byte>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien PDF indirme hatası: {UUID}", uuid);
            throw;
        }
    }

    public async Task<string> GetInvoiceXmlAsync(string uuid, bool isOutbox)
    {
        _logger.LogDebug("Bien XML indirme: {UUID}, Giden: {IsOutbox}", uuid, isOutbox);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            InvoiceDataResponse response;
            if (isOutbox)
            {
                response = await client.GetOutboxInvoiceDataAsync(uuid);
            }
            else
            {
                response = await client.GetInboxInvoiceDataAsync(uuid);
            }

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien XML indirme hatası: {Message}", response.Message);
                throw new InvalidOperationException($"XML indirilemedi: {response.Message}");
            }

            if (response.Value?.Data == null)
            {
                return string.Empty;
            }

            return Encoding.UTF8.GetString(response.Value.Data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien XML indirme hatası: {UUID}", uuid);
            throw;
        }
    }

    #endregion

    #region Durum Sorgulama

    public async Task<EInvoiceStatusResult> GetInvoiceStatusAsync(string uuid, bool isOutbox)
    {
        _logger.LogDebug("Bien durum sorgulama: {UUID}, Giden: {IsOutbox}", uuid, isOutbox);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            InvoiceStatusResponse response;
            if (isOutbox)
            {
                response = await client.QueryOutboxInvoiceStatusAsync(new[] { uuid });
            }
            else
            {
                response = await client.QueryInboxInvoiceStatusAsync(new[] { uuid });
            }

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien durum sorgulama hatası: {Message}", response.Message);
                return new EInvoiceStatusResult
                {
                    Success = false,
                    ETTN = uuid,
                    ErrorMessage = response.Message,
                    RawResponse = JsonConvert.SerializeObject(response)
                };
            }

            var statusInfo = response.Value?.FirstOrDefault();
            if (statusInfo == null)
            {
                return new EInvoiceStatusResult
                {
                    Success = false,
                    ETTN = uuid,
                    ErrorMessage = "Durum bilgisi bulunamadı"
                };
            }

            var result = BienMapper.ToEInvoiceStatusResult(statusInfo);
            result.RawResponse = JsonConvert.SerializeObject(response);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien durum sorgulama hatası: {UUID}", uuid);
            return new EInvoiceStatusResult
            {
                Success = false,
                ETTN = uuid,
                ErrorMessage = ex.Message
            };
        }
    }

    #endregion

    #region Gelen Fatura İşlemleri (Kabul/Red)

    public async Task<bool> AcceptInvoiceAsync(string uuid)
    {
        _logger.LogDebug("Bien fatura kabul: {UUID}", uuid);
        return await SendDocumentResponseAsync(uuid, DocumentResponseStatus.Approved, null);
    }

    public async Task<bool> RejectInvoiceAsync(string uuid, string reason)
    {
        _logger.LogDebug("Bien fatura red: {UUID}, Neden: {Reason}", uuid, reason);
        return await SendDocumentResponseAsync(uuid, DocumentResponseStatus.Declined, reason);
    }

    private async Task<bool> SendDocumentResponseAsync(
        string uuid,
        DocumentResponseStatus responseStatus,
        string? reason)
    {
        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var responseInfo = new DocumentResponseInfo
            {
                InvoiceId = uuid,
                ResponseStatus = responseStatus,
                Reason = reason
            };

            var response = await client.SendDocumentResponseAsync(new[] { responseInfo });

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien fatura yanıt gönderme hatası: {Message}", response.Message);
                return false;
            }

            // FlagResponse dönüyor, Value boolean
            return response.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien fatura yanıt gönderme hatası: {UUID}", uuid);
            return false;
        }
    }

    public async Task<bool> ReceiptInvoiceAsync(string uuid)
    {
        _logger.LogDebug("Bien fatura alındı işareti: {UUID}", uuid);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var response = await client.SetInvoicesTakenAsync(new[] { uuid });

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien fatura alındı işaretleme hatası: {Message}", response.Message);
                return false;
            }

            return response.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien fatura alındı işaretleme hatası: {UUID}", uuid);
            return false;
        }
    }

    #endregion

    #region E-Arşiv İptal

    public async Task<bool> CancelEArchiveAsync(string uuid, DateTime date, string type, string reason)
    {
        _logger.LogDebug("Bien E-Arşiv iptal: {UUID}, Tip: {Type}", uuid, type);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var cancelRequest = new EArchiveCancelInvoiceContext
            {
                InvoiceId = uuid,
                CancelDate = date
            };

            var response = await client.CancelEArchiveInvoiceAsync(cancelRequest);

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien E-Arşiv iptal hatası: {Message}", response.Message);
                return false;
            }

            return response.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien E-Arşiv iptal hatası: {UUID}", uuid);
            return false;
        }
    }

    #endregion
}
