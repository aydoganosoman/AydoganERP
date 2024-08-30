namespace AydoganERP.Base.Application.Models;

public class TokenModel
{
    public TokenModel(string token, string refreshToken)
    {
        this.Token = token;
        this.RefreshToken = refreshToken;
    }

    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
