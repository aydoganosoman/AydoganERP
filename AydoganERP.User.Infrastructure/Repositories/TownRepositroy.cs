using AydoganERP.Base.Application.Repositories;
using AydoganERP.Base.Application.Repositories.TownRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Infrastructure.Persistence;

namespace AydoganERP.User.Application.Repositories.InboxRepository;
public class TownRepositroy : EfRepositoryBase<Town, Town, int, ApplicationDbContext>, ITownRepositroy
{
    public TownRepositroy(ApplicationDbContext context)
        : base(context) { }

    public async Task<int> GetMaxIdAsync()
    {
        return Context.Towns.Max(x => x.Id);
    }
}
