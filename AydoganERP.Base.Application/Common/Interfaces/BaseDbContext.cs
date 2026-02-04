using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AydoganERP.Base.Application.Common.Interfaces;

public interface IBaseDbContext
{
    DbSet<Country> Countries { get; set; }
    DbSet<City> Cities { get; set; }
    DbSet<District> Districts { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Company> Companies { get; set; }
    // DbSet<Customer> Customers { get; set; }
    // DbSet<Product> Products { get; set; }
    // DbSet<StockMovement> StockMovements { get; set; }
    DbSet<UserActionLog> UserActionLogs { get; set; }
    DbSet<AuditLog> AuditLogs { get; set; }

    ChangeTracker ChangeTracker
    {
        get;
    }
    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}