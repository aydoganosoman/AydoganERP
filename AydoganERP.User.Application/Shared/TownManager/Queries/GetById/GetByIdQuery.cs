using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.TownManager.Queries.GetById;

public class GetByIdQuery : IRequest<Town>
{
    public int Id { get; set; }
}
