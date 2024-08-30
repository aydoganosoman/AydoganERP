using AydoganERP.Base.Application.Models;
using AydoganERP.User.Api.Services;
using AydoganERP.User.Application.Users.UserManager.Commands.UpdateRefreshToken;
using AydoganERP.User.Application.Users.UserManager.Queries.GetUserByEMailAndPassword;

namespace AydoganERP.User.Api.Controllers.Auth;

public class LoginEndpoint : BaseEndpoint<GetUserByEMailAndPasswordQuery, UserAuthModel>
{
    private readonly IUserService _userService;
    public LoginEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Post("Auth/Login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserByEMailAndPasswordQuery req, CancellationToken ct)
    {
        var userVm = await Mediator.Send(req);

        userVm.Token = _userService.Authenticate(userVm);

        await Mediator.Send(new UpdateRefreshTokenCommand
        {
            ApiKey = userVm.ApiKey,
            RefreshToken = userVm.Token.RefreshToken
        });

        await SendAsync(userVm);
    }
}
