using AydoganERP.Base.Domain.Entities;

namespace AydoganERP.Base.Application.Repositories.CategoryRepository;
public interface ICategoryRepository : IAsyncRepository<Category, CategoryDto, Guid>, IRepository<Category, CategoryDto, Guid> { }