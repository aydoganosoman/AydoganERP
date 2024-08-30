using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.TownManager.Queries.GetByCityId;

public class GetByCityIdQuery : IRequest<GetListResponse<Town>>
{
    public int CityId { get; set; }
}
