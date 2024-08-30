using AydoganERP.Base.Application.Repositories;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Infrastructure.Persistence;

namespace AydoganERP.User.Application.Repositories.InboxRepository;
public class CategoryRepository : EfRepositoryBase<Category, CategoryDto, Guid, ApplicationDbContext>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context)
        : base(context) { }
}
