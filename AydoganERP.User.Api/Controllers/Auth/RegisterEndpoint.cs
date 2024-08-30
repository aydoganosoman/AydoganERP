using AydoganERP.User.Application.Users.UserManager.Commands.Register;

namespace AydoganERP.User.Api.Controllers.Auth;

public class RegisterEndpoint : BaseEndpoint<RegisterCommand, Guid>
{
    public RegisterEndpoint()
    {
    }

    public override void Configure()
    {
        Post("Auth/Register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterCommand req, CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(req));
    }
}
