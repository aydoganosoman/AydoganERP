using System.Net.Http.Headers;
using AydoganERP.EInvoice.MySoft.Models;
using Newtonsoft.Json;

namespace AydoganERP.EInvoice.MySoft.Services;

/// <summary>
/// GİB hesap sorgulama servisi
/// </summary>
public class GibAccountService
{
    private readonly TokenService _tokenService;
    private readonly Uri _baseUri;
    private readonly HttpClient _httpClient;

    public GibAccountService(TokenService tokenService, Uri baseUri)
    {
        _tokenService = tokenService;
        _baseUri = baseUri;
        _httpClient = new HttpClient { BaseAddress = baseUri };
    }

    public async Task<HttpClientResult<GibAccountResponse>> GetGibAccountAsync(string taxNumber)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _tokenService.Token);

            var response = await _httpClient.GetAsync($"/api/GibAccount/GetByIdentifier?identifier={taxNumber}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<GibAccountResponse>(responseContent);
                return HttpClientResult<GibAccountResponse>.Success(result!);
            }

            return HttpClientResult<GibAccountResponse>.Error(responseContent);
        }
        catch (Exception ex)
        {
            return HttpClientResult<GibAccountResponse>.Error(ex.Message);
        }
    }
}

public class GibAccountResponse
{
    public string? VknTckn { get; set; }
    public string? Title { get; set; }
    public bool IsEInvoiceUser { get; set; }
    public bool IsEArchiveUser { get; set; }
    public bool IsEWaybillUser { get; set; }
    public string? FirstCreationTime { get; set; }
    public List<AliasInfo>? Aliases { get; set; }
}

public class AliasInfo
{
    public string? Alias { get; set; }
    public string? Type { get; set; } // PK, GB
}
