using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.CityManager.Queries.GetById;

public class GetByIdQuery : IRequest<City>
{
    public int Id { get; set; }
}
