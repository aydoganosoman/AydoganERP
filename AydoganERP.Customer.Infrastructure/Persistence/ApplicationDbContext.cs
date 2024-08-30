using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Persistence;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AydoganERP.Customer.Infrastructure.Persistence;

public class ApplicationDbContext : BaseDbContext
{
    public DbSet<AuthorizePerson> AuthorizePeople { get; set; }
    public DbSet<Domain.Entities.Customer> Customers { get; set; }
    public DbSet<IBAN> IBANs { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CustomerSegmentation> CustomerSegmentations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<QuoteDetail> QuoteDetails { get; set; }
    public DbSet<SalesOpportunity> SalesOpportunities { get; set; }
    public DbSet<Support> Supports { get; set; }

    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTime;
    public ApplicationDbContext(
        DbContextOptions options,
        ICurrentUserService currentUserService,
        IDateTimeService dateTime) : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    #region Migration
    //dotnet ef migrations add Initialize --startup-project AydoganERP.Customer.Api --project AydoganERP.Customer.Infrastructure --output-dir Persistence/Migrations 
    //dotnet ef database update --startup-project AydoganERP.Customer.Api --project AydoganERP.Customer.Infrastructure
    //dotnet ef migrations remove --startup-project AydoganERP.Customer.Api --project AydoganERP.Customer.Infrastructure
    //public ApplicationDbContext(DbContextOptions options) : base(options)
    //{

    //}

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql("Host=192.168.1.254;Port=5432;Database=TestCustomerDb;Username=postgres;Password=1211_1211;");
    //}
    #endregion

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<Entity<Guid>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserApiKey;
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserApiKey;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
