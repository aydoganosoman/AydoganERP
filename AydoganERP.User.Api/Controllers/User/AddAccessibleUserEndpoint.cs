using AydoganERP.User.Application.Users.UserManager.Commands.AddAccessibleUser;

namespace AydoganERP.User.Api.Controllers.User;

public class AddAccessibleUserEndpoint : BaseEndpointWithoutResponse<AddAccessibleUserCommand>
{
    public AddAccessibleUserEndpoint()
    {
    }

    public override void Configure()
    {
        Post("User/AddAccessibleUser");
    }

    public override async Task HandleAsync(AddAccessibleUserCommand req, CancellationToken ct)
    {
        await SendAsync(Mediator.Send(req));
    }
}
