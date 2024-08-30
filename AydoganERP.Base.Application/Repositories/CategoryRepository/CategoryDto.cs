using AydoganERP.Base.Application.Mappings;
using AydoganERP.Base.Domain.Entities;

namespace AydoganERP.Base.Application.Repositories.CategoryRepository;

public class CategoryDto : IMapFrom<Category>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}
