using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Application.Shared.CityManager.Queries.GetById;

namespace AydoganERP.User.Api.Controllers.Shared;

public class GetCityByIdEndpoint : BaseEndpoint<GetByIdQuery, City>
{
    public GetCityByIdEndpoint()
    {
        
    }

    public override void Configure()
    {
        Get("Shared/GetCityById");
    }

    public override async Task HandleAsync(GetByIdQuery req, CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(req));
    }
}
