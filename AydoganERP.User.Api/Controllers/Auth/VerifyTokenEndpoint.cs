using AydoganERP.Base.Application.Models;
using AydoganERP.User.Api.Services;

namespace AydoganERP.User.Api.Controllers.Auth;

public class VerifyTokenEndpoint : BaseEndpoint<TokenModel, UserAuthModel>
{
    private readonly IUserService _userService;
    public VerifyTokenEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Post("Auth/VerifyToken");
    }

    public override async Task HandleAsync(TokenModel req, CancellationToken ct)
    {
        await SendAsync(await _userService.VerifyToken(req));
    }
}
