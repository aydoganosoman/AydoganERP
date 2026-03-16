namespace AydoganERP.EInvoice.MySoft.Services;

/// <summary>
/// MySoft API token yönetimi
/// </summary>
public class TokenService
{
    private string? _token;
    private DateTime _tokenExpiry;

    public string? Token => _token;

    public void SetToken(string token, int expiresInMinutes = 55)
    {
        _token = token;
        _tokenExpiry = DateTime.UtcNow.AddMinutes(expiresInMinutes);
    }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(_token) && DateTime.UtcNow < _tokenExpiry;
    }

    public void Clear()
    {
        _token = null;
        _tokenExpiry = DateTime.MinValue;
    }
}
