using AydoganERP.Base.Application.Repositories.CategoryRepository;
using MediatR;

namespace AydoganERP.User.Application.Shared.CategoryManager.Commands.Create;

public class CreateCommand : IRequest<CategoryDto>
{
    public string Name { get; set; }
    public string Color { get; set; }
}
