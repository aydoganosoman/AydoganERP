using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using AydoganERP.EInvoice.Abstractions.Models.Settings;
using AydoganERP.EInvoice.MySoft.Models;
using AydoganERP.EInvoice.MySoft.Models.Request;
using AydoganERP.EInvoice.MySoft.Models.Response;
using Newtonsoft.Json;

namespace AydoganERP.EInvoice.MySoft.Services;

/// <summary>
/// MySoft fatura servisi
/// </summary>
public class InvoiceService
{
    private readonly TokenService _tokenService;
    private readonly Uri _baseUri;
    private readonly MySoftSettings _settings;
    private readonly HttpClient _httpClient;

    public InvoiceService(TokenService tokenService, Uri baseUri, MySoftSettings settings)
    {
        _tokenService = tokenService;
        _baseUri = baseUri;
        _settings = settings;
        _httpClient = new HttpClient { BaseAddress = baseUri };
    }

    private void SetAuthHeader()
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _tokenService.Token);
    }

    #region Fatura Gönderimi

    public async Task<HttpClientResult<InvoiceOutboxResponse>> SendInvoiceAsync(InvoiceOutboxRequest request)
    {
        try
        {
            SetAuthHeader();

            var content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("/api/InvoiceOutbox/Send", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<InvoiceOutboxResponse>(responseContent);
                return HttpClientResult<InvoiceOutboxResponse>.Success(result!);
            }

            return HttpClientResult<InvoiceOutboxResponse>.Error(responseContent);
        }
        catch (Exception ex)
        {
            return HttpClientResult<InvoiceOutboxResponse>.Error(ex.Message);
        }
    }

    #endregion

    #region Gelen Faturalar

    public async Task<HttpClientResult<List<IncomingInvoiceResponse>>> GetIncomingInvoicesAsync(
        DateTime startDate,
        DateTime endDate,
        string? pkAlias = null)
    {
        try
        {
            SetAuthHeader();

            var request = new
            {
                StartDate = startDate.ToString("yyyy-MM-dd"),
                EndDate = endDate.ToString("yyyy-MM-dd"),
                PkAlias = pkAlias,
                IsUblNull = false,
                Skip = 0,
                Page = 0,
                PageSize = 100
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("/api/InvoiceInbox/GetList", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<IncomingInvoiceResponse>>(responseContent);
                return HttpClientResult<List<IncomingInvoiceResponse>>.Success(result!);
            }

            return HttpClientResult<List<IncomingInvoiceResponse>>.Error(responseContent);
        }
        catch (Exception ex)
        {
            return HttpClientResult<List<IncomingInvoiceResponse>>.Error(ex.Message);
        }
    }

    #endregion

    #region Durum Sorgulama

    public async Task<HttpClientResult<InvoiceStatusResponse>> GetInvoiceStatusAsync(string uuid, bool isOutbox)
    {
        try
        {
            SetAuthHeader();

            var endpoint = isOutbox
                ? $"/api/InvoiceOutbox/GetStatus?ettn={uuid}"
                : $"/api/InvoiceInbox/GetStatus?ettn={uuid}";

            var response = await _httpClient.GetAsync(endpoint);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<InvoiceStatusResponse>(responseContent);
                return HttpClientResult<InvoiceStatusResponse>.Success(result!);
            }

            return HttpClientResult<InvoiceStatusResponse>.Error(responseContent);
        }
        catch (Exception ex)
        {
            return HttpClientResult<InvoiceStatusResponse>.Error(ex.Message);
        }
    }

    #endregion

    #region PDF/XML İndirme

    public async Task<byte[]> GetInvoicePdfAsync(string uuid, bool isOutbox)
    {
        SetAuthHeader();

        var endpoint = isOutbox
            ? $"/api/InvoiceOutbox/GetPdf?ettn={uuid}"
            : $"/api/InvoiceInbox/GetPdf?ettn={uuid}";

        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var zipBytes = await response.Content.ReadAsByteArrayAsync();

        // ZIP'ten PDF'i çıkar
        using var zipStream = new MemoryStream(zipBytes);
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);

        var pdfEntry = archive.Entries.FirstOrDefault(e => e.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase));
        if (pdfEntry == null)
        {
            throw new InvalidOperationException("PDF dosyası ZIP içinde bulunamadı");
        }

        using var pdfStream = pdfEntry.Open();
        using var memoryStream = new MemoryStream();
        await pdfStream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public async Task<string> GetInvoiceXmlAsync(string uuid, bool isOutbox)
    {
        SetAuthHeader();

        var endpoint = isOutbox
            ? $"/api/InvoiceOutbox/GetUblXml?ettn={uuid}"
            : $"/api/InvoiceInbox/GetUblXml?ettn={uuid}";

        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var zipBytes = await response.Content.ReadAsByteArrayAsync();

        // ZIP'ten XML'i çıkar
        using var zipStream = new MemoryStream(zipBytes);
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);

        var xmlEntry = archive.Entries.FirstOrDefault(e => e.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase));
        if (xmlEntry == null)
        {
            throw new InvalidOperationException("XML dosyası ZIP içinde bulunamadı");
        }

        using var xmlStream = xmlEntry.Open();
        using var reader = new StreamReader(xmlStream);
        return await reader.ReadToEndAsync();
    }

    #endregion

    #region Gelen Fatura İşlemleri

    public async Task<bool> AcceptInvoiceAsync(string uuid)
    {
        SetAuthHeader();

        var response = await _httpClient.PostAsync($"/api/InvoiceInbox/Accept?ettn={uuid}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RejectInvoiceAsync(string uuid, string reason)
    {
        SetAuthHeader();

        var request = new { Ettn = uuid, DeclineReason = reason };
        var content = new StringContent(
            JsonConvert.SerializeObject(request),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("/api/InvoiceInbox/Decline", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ReceiptInvoiceAsync(string uuid)
    {
        SetAuthHeader();

        var response = await _httpClient.PostAsync($"/api/InvoiceInbox/Receipt?ettn={uuid}", null);
        return response.IsSuccessStatusCode;
    }

    #endregion

    #region E-Arşiv İptal

    public async Task<bool> CancelEArchiveAsync(string uuid, DateTime date, string type, string reason)
    {
        SetAuthHeader();

        var request = new
        {
            Ettn = uuid,
            CancelDate = date.ToString("yyyy-MM-dd HH:mm"),
            CancelType = type,
            CancelNote = reason
        };

        var content = new StringContent(
            JsonConvert.SerializeObject(request),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("/api/InvoiceOutbox/CancelEArchive", content);
        return response.IsSuccessStatusCode;
    }

    #endregion
}
