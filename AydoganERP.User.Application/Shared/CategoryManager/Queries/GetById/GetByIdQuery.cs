using AydoganERP.Base.Application.Repositories.CategoryRepository;
using MediatR;

namespace AydoganERP.User.Application.Shared.CategoryManager.Queries.GetById;

public class GetByIdQuery : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
}
