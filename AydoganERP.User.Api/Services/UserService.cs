using AydoganERP.Base.Application.Models;
using AydoganERP.User.Application.Common.Interfaces;
using AydoganERP.User.Application.Users.UserManager.Queries.VerifyToken;
using MediatR;
using System.Security.Claims;

namespace AydoganERP.User.Api.Services;

public interface IUserService
{
    TokenModel Authenticate(UserAuthModel user);
    bool Logout();
    Task<UserAuthModel> VerifyToken(TokenModel token);
}

public class UserService : IUserService
{
    private IHttpContextAccessor _httpContextAccessor;
    private ITokenService _tokenService;
    private IMediator _mediator;
    public UserService(IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IMediator mediator)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenService = tokenService;
        _mediator = mediator;
    }

    public TokenModel Authenticate(UserAuthModel user)
    {
        // return null if user not found
        if (user == null)
            throw new Exception("Kullanıcı bulunamadı!");

        if (user.IsEnable == false)
            throw new Exception("Kullanıcı pasif!");

        var jwtToken = _tokenService.GenerateAccessToken(new Claim[]
             {
                new Claim(ClaimTypes.Email, user.EMail),
                new Claim(ClaimTypes.Name, user.ApiKey)
             });

        var refreshToken = _tokenService.GenerateRefreshToken();

        return new TokenModel(jwtToken, refreshToken);
    }

    public bool Logout()
    {
        throw new NotImplementedException();
    }

    public async Task<UserAuthModel> VerifyToken(TokenModel token)
    {
        var isValidUser = await _mediator.Send(new VerifyTokenQuery()
        {
            RefreshToken = token.RefreshToken
        });
        isValidUser.Token = token;

        if (isValidUser != null)
        {
            _tokenService.GetPrincipalFromExpiredToken(token.Token);
        }

        return isValidUser;
    }
}
