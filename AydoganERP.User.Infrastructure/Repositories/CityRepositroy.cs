using AydoganERP.Base.Application.Repositories;
using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Infrastructure.Persistence;
using System.Linq.Dynamic.Core;

namespace AydoganERP.User.Application.Repositories.InboxRepository;
public class CityRepositroy : EfRepositoryBase<City, City, int, ApplicationDbContext>, ICityRepositroy
{
    public CityRepositroy(ApplicationDbContext context)
        : base(context) { }

    public async Task<int> GetMaxIdAsync()
    {
        return Context.Cities.Max(x => x.Id);
    }

}
