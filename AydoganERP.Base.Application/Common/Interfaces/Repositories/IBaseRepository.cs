using System.Linq.Expressions;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Base.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.Common.Interfaces.Repositories;
public interface IBaseRepository<TEntity> where TEntity : Entity
{
    DbContext GetDbContext();

    DbSet<TEntity> GetDbSetAsync();

    IQueryable<TEntity> GetQueryable();
    
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        string orderColumn = "",
        bool orderByAsc = true,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        string orderColumn = "",
        bool orderByAsc = true,
        int limit = 0,
        CancellationToken cancellationToken = default);

    Task<PaginatedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        string orderColumn = "",
        bool orderByAsc = true,
        int limit = 0,
        int currentPage = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, string[] properties = null, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}
