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
            var request = new
            {
                UserName = userName,
                Password = password
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("/api/Auth/Login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                if (result?.Token != null)
                {
                    _tokenService.SetToken(result.Token);
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
        public string? Token { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
