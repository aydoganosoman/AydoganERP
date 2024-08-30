using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.TownManager.Commands.Create;

public class CreateCommand : IRequest<Town>
{
    public int CityId { get; set; }
    public string Name { get; set; }
}
