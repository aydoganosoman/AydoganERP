using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Application.Shared.TownManager.Commands.Create;

namespace AydoganERP.User.Api.Controllers.Shared;

public class CreateTownEndpoint : BaseEndpoint<Application.Shared.TownManager.Commands.Create.CreateCommand, Town>
{
    public CreateTownEndpoint()
    {
    }

    public override void Configure()
    {
        Post("Shared/CreateTown");
    }

    public override async Task HandleAsync(CreateCommand req, CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(req));
    }
}
