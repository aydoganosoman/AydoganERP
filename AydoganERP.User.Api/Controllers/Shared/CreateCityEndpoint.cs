using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Application.Shared.CityManager.Commands.Create;

namespace AydoganERP.User.Api.Controllers.Shared;

public class CreateCityEndpoint : BaseEndpoint<CreateCommand, City>
{
    public CreateCityEndpoint()
    {
        
    }

    public override void Configure()
    {
        Post("Shared/CreateCity");
    }

    public override async Task HandleAsync(CreateCommand req, CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(req));
    }
}
