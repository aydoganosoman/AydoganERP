using AydoganERP.Base.Domain.Modules.IdentityModule.Enums;
using AydoganERP.Identity.Application.Interfaces;
using AydoganERP.Identity.Application.Models;
using AydoganERP.Identity.Application.UserManager.Queries.VerifyToken;
using MediatR;
using System.Security.Claims;

namespace AydoganERP.Api.Services;

public interface IUserService
{
    TokenModel Authenticate(UserAuthModel user);
    bool Logout();
    Task<UserAuthModel> VerifyToken(TokenModel token);
}

public class UserService : IUserService
{
    private ITokenService _tokenService;
    private IMediator _mediator;

    public UserService(ITokenService tokenService,
        IMediator mediator)
    {
        _tokenService = tokenService;
        _mediator = mediator;
    }

    public TokenModel Authenticate(UserAuthModel user)
    {
        // return null if user not found
        if (user == null)
            throw new Exception("Kullanıcı bulunamadı!");

        if (user.Status == UserStatusEnum.Passive)
            throw new Exception("Kullanıcı pasif!");

        var jwtToken = _tokenService.GenerateAccessToken(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
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
        var isValidUser = await _mediator.Send(new VerifyTokenQuery(token.RefreshToken));
        isValidUser.Token = token;

        if (isValidUser != null)
        {
            _tokenService.GetPrincipalFromExpiredToken(token.Token);
        }

        return isValidUser;
    }
}