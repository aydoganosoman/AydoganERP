using System.Net.Http.Headers;
using System.Text;
using AydoganERP.EInvoice.MySoft.Models;
using Newtonsoft.Json;

namespace AydoganERP.EInvoice.MySoft.Services;

/// <summary>
/// MySoft kimlik doğrulama servisi
/// </summary>
public class AuthService
{
    private readonly TokenService _tokenService;
    private readonly Uri _baseUri;
    private readonly HttpClient _httpClient;

    public AuthService(TokenService tokenService, Uri baseUri)
    {
        _tokenService = tokenService;
        _baseUri = baseUri;
        _httpClient = new HttpClient { BaseAddress = baseUri };
    }

    public async Task<bool> LoginAsync(string userName, string password)
    {
        try
        {
            var postData = new Dictionary<string, string>
            {
                { "username", userName },
                { "password", password },
                { "grant_type", "password" }
            };

            var content = new FormUrlEncodedContent(postData);

            var response = await _httpClient.PostAsync("/oauth/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                if (result?.access_token != null)
                {
                    _tokenService.SetToken(result.access_token);
                    return true;
                }
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    private class LoginResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}
