using AydoganERP.Base.Application.Repositories.CategoryRepository;
using MediatR;

namespace AydoganERP.User.Application.Shared.CategoryManager.Commands.Update;

public class UpdateCommand : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}
