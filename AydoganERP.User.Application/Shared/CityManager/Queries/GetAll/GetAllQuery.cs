using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.CityManager.Queries.GetAll;

public class GetAllQuery : IRequest<GetListResponse<City>>
{
}
