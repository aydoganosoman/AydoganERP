using AydoganERP.User.Application.Common.Models;
using AydoganERP.User.Application.Companies.CompanyManager.Commands.Update;

namespace AydoganERP.User.Api.Controllers.Company;

public class UpdateEndpoint : BaseEndpoint<UpdateCommand, CompanyDto>
{
    public UpdateEndpoint()
    { }

    public override void Configure()
    {
        Patch("Company/Update");
    }

    public override async Task HandleAsync(UpdateCommand req, CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(req));
    }
}
