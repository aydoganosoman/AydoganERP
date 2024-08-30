using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using MediatR;

namespace AydoganERP.User.Application.Shared.CategoryManager.Queries.GetAll;

public class GetAllQuery : IRequest<GetListResponse<CategoryDto>>
{
}
