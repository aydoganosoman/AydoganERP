using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.CityManager.Commands.Create;

public class CreateCommand : IRequest<City>
{
    public string Name { get; set; }
}
