using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Infrastructure.Persistence;
using AydoganERP.Base.Infrastructure.Repositories;
using AydoganERP.Inventory.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Infrastructure.Repositories;
public class InventoryRepository : BaseRepository<Base.Domain.Modules.InventoryModule.Entities.Product>, IInventoryRepository
{
    public InventoryRepository(DbContextOptions<ApplicationDbContext> dbContextOptions,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService) : base(dbContextOptions, currentUserService, dateTimeService)
    {

    }
}
