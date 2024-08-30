using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Persistence;
using AydoganERP.Base.Domain.Common;
using AydoganERP.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AydoganERP.User.Infrastructure.Persistence;

public class ApplicationDbContext : BaseDbContext
{
    public DbSet<Domain.Entities.User> Users { get; set; }
    public DbSet<UserAuthorization> UserAuthorizations { get; set; }
    public DbSet<UserToRole> UserToRoles { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyUserAuthorization> CompanyUserAuthorizations { get; set; }

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
    //dotnet ef migrations add Initialize --startup-project AydoganERP.User.Api --project AydoganERP.User.Infrastructure --output-dir Persistence/Migrations 
    //dotnet ef database update --startup-project AydoganERP.User.Api --project AydoganERP.User.Infrastructure
    //dotnet ef migrations remove --startup-project AydoganERP.User.Api --project AydoganERP.User.Infrastructure
    //public ApplicationDbContext()
    //{

    //}

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql("Host=192.168.1.254;Port=5432;Database=TestUserDb;Username=postgres;Password=1211_1211;");
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
