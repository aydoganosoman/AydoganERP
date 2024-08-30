using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Application.Shared.TownManager.Queries.GetById;

namespace AydoganERP.User.Api.Controllers.Shared;

public class GetTownByIdEndpoint : BaseEndpoint<GetByIdQuery, Town>
{
    public GetTownByIdEndpoint()
    {
        
    }

    public override void Configure()
    {
        Get("Shared/GetTownById");
    }

    public override async Task HandleAsync(GetByIdQuery req, CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(req));
    }
}
