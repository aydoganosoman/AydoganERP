using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Interfaces.Repositories;
using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Application.Common.Paging;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AydoganERP.Base.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
{
    public readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
    public readonly ICurrentUserService _currentUserService;
    public readonly IDateTimeService _dateTimeService;

    public BaseRepository(DbContextOptions<ApplicationDbContext> dbContextOptions,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService)
    {
        _dbContextOptions = dbContextOptions;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<TEntity> AddAsync(TEntity entity, string[] properties = null,
        CancellationToken cancellationToken = default)
    {
        using (ApplicationDbContext _context =
               new ApplicationDbContext(_dbContextOptions, _currentUserService, _dateTimeService))
        {
            var _querable = _context.Set<TEntity>();

            if (properties != null)
                foreach (var propertyItem in properties)
                {
                    var _propertyValue = typeof(TEntity).GetProperty(propertyItem).GetValue(entity);

                    _querable.Attach((TEntity)_propertyValue);
                }

            await _context.SaveChangesAsync();
        }

        return entity;
    }

    public IQueryable<TEntity> GetQueryable()
    {
        using (ApplicationDbContext _context =
               new ApplicationDbContext(_dbContextOptions, _currentUserService, _dateTimeService))
        {
            var _querable = _context.Set<TEntity>();
            return _querable;
        }
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        string orderColumn = "",
        bool orderByAsc = true,
        CancellationToken cancellationToken = default)
    {
        TEntity result = null;

        try
        {
            using (ApplicationDbContext _context =
                   new ApplicationDbContext(_dbContextOptions, _currentUserService, _dateTimeService))
            {
                var query = _context.Set<TEntity>()
                    .AsQueryable();

                if (expression != null)
                    query = query
                        .Where(expression);

                if (includes != null)
                    foreach (string includeItem in includes)
                    {
                        if (!string.IsNullOrEmpty(includeItem) && !string.IsNullOrWhiteSpace(includeItem))
                            query = query
                                .Include(includeItem);
                    }

                if (orderByAsc == false)
                    query = IQueryableExtensions
                        .OrderBy(query, orderColumn, orderByAsc);

                result = query
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return result;
    }

    public DbContext GetDbContext()
    {
        return new ApplicationDbContext(_dbContextOptions, _currentUserService, _dateTimeService);
    }

    public DbSet<TEntity> GetDbSetAsync()
    {
        using (ApplicationDbContext _context =
               new ApplicationDbContext(_dbContextOptions, _currentUserService, _dateTimeService))
        {
            return _context.Set<TEntity>();
        }
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        string orderColumn = "",
        bool orderByAsc = true,
        int limit = 0,
        CancellationToken cancellationToken = default)
    {
        List<TEntity> result = new List<TEntity>();

        try
        {
            using (ApplicationDbContext _context =
                   new ApplicationDbContext(_dbContextOptions, _currentUserService, _dateTimeService))
            {
                var query = _context.Set<TEntity>()
                    .AsQueryable();

                if (expression != null)
                    query = query
                        .Where(expression);

                if (includes != null)
                    foreach (string includeItem in includes)
                    {
                        if (!string.IsNullOrEmpty(includeItem) && !string.IsNullOrWhiteSpace(includeItem))
                            query = query
                                .Include(includeItem);
                    }

                if (orderByAsc == false)
                    query = IQueryableExtensions
                        .OrderBy(query, orderColumn, orderByAsc);

                if (limit > 0)
                    query = query
                        .Take(limit);

                result = query
                    .AsNoTracking()
                    .ToList();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return result;
    }

    public async Task<PaginatedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        string orderColumn = "",
        bool orderByAsc = true,
        int limit = 0,
        int currentPage = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        PaginatedList<TEntity> result = default(PaginatedList<TEntity>);

        try
        {
            using (ApplicationDbContext _context =
                   new ApplicationDbContext(_dbContextOptions, _currentUserService, _dateTimeService))
            {
                var query = _context.Set<TEntity>()
                    .AsQueryable();

                if (expression != null)
                    query = query
                        .Where(expression);

                if (includes != null)
                    foreach (string includeItem in includes)
                    {
                        if (!string.IsNullOrEmpty(includeItem) && !string.IsNullOrWhiteSpace(includeItem))
                            query = query
                                .Include(includeItem);
                    }

                if (orderByAsc == false)
                    query = IQueryableExtensions
                        .OrderBy(query, orderColumn, orderByAsc);

                if (limit > 0)
                    query = query
                        .Take(limit);

                result = await query
                    .AsNoTracking()
                    .ToPagedListAsync(currentPage, pageSize, "", "");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return result;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using (ApplicationDbContext _context =
               new ApplicationDbContext(_dbContextOptions, _currentUserService, _dateTimeService))
        {
            DbSet<TEntity> table = _context.Set<TEntity>();

            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        return entity;
    }
}