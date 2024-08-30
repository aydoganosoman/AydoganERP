using AydoganERP.Base.Application.Models;
using AydoganERP.User.Application.Common.Models;
using AydoganERP.User.Application.Users.UserManager.Queries.GetAll;

namespace AydoganERP.User.Api.Controllers.User;

public class GetAllEndpoint : BaseEndpointWithoutRequest<GetListResponse<UserDto>>
{
    public GetAllEndpoint()
    {
        
    }

    public override void Configure()
    {
        Get("User/GetAll");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(new GetAllQuery()));
    }
}
