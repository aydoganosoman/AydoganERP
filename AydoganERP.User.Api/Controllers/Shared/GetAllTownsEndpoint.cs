using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Application.Shared.TownManager.Queries.GetAll;

namespace AydoganERP.User.Api.Controllers.Shared;

public class GetAllTownsEndpoint : BaseEndpointWithoutRequest<GetListResponse<Town>>
{
    public GetAllTownsEndpoint()
    {
        
    }

    public override void Configure()
    {
        Get("Shared/GetAllTowns");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(new GetAllQuery()));
    }
}
