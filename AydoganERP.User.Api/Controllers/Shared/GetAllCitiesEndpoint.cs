using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Application.Shared.CityManager.Queries.GetAll;

namespace AydoganERP.User.Api.Controllers.Shared;

public class GetAllCitiesEndpoint : BaseEndpointWithoutRequest<GetListResponse<City>>
{
    public GetAllCitiesEndpoint()
    {
        
    }

    public override void Configure()
    {
        Get("Shared/GetAllCities");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(new GetAllQuery()));
    }
}
