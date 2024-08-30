using AydoganERP.User.Application.Users.UserManager.Commands.ForgotPassword;

namespace AydoganERP.User.Api.Controllers.Auth;

public class ForgotPasswordEndpoint : BaseEndpointWithoutResponse<ForgotPasswordCommand>
{
    public ForgotPasswordEndpoint()
    {
        
    }

    public override void Configure()
    {
        Post("Auth/ForgotPassword");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ForgotPasswordCommand req, CancellationToken ct)
    {
        await SendAsync(Mediator.Send(req, ct));
    }
}
