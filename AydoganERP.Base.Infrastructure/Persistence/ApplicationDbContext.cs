using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using CommentAttribute = AydoganERP.Base.Domain.Common.CommentAttribute;

namespace AydoganERP.Base.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IBaseDbContext
{
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

    //dotnet ef migrations add Initialize --startup-project AydoganERP.APi --project AydoganERP.Base.Infrastructure --context ApplicationDbContext --output-dir Persistence/Migrations 
    //dotnet ef database update --startup-project AydoganERP.Api --project AydoganERP.Base.Infrastructure --context ApplicationDbContext
    //dotnet ef migrations remove --startup-project AydoganERP.Api --project AydoganERP.Base.Infrastructure --context ApplicationDbContext
    // public ApplicationDbContext()
    // {
    //
    // }
    //
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=container_manager;Username=postgres;Password=1211_1211;");
    //     //optionsBuilder.UseNpgsql("Host=dpg-cv3nl2lds78s73dvavqg-a;Port=5432;Database=hera_lojistik_db;Username=hera_lojistik_db_user;Password=FT4l501vUrYz4zmPqUjh5sfLaaWbxNGN;");
    // }

    #endregion

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        var auditEntries = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserEmail;
                    entry.Entity.Created ??= _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserEmail;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }

            if (entry.State == EntityState.Added ||
                entry.State == EntityState.Modified ||
                entry.State == EntityState.Deleted)
            {
                var audit = new AuditLog
                {
                    TableName = entry.Entity.GetType().Name,
                    UserEmail = _currentUserService.UserEmail,
                    Date = _dateTime.Now,
                    KeyValues = JsonSerializer.Serialize(
                        entry.Properties.Where(p => p.Metadata.IsPrimaryKey())
                            .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)
                    )
                };

                if (entry.State == EntityState.Added)
                {
                    audit.Action = "Insert";
                    audit.NewValues = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                }
                else if (entry.State == EntityState.Deleted)
                {
                    audit.Action = "Delete";
                    audit.OldValues = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                }
                else if (entry.State == EntityState.Modified)
                {
                    audit.Action = "Update";
                    audit.OldValues = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                    audit.NewValues = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                }

                auditEntries.Add(audit);
            }
        }
        
        var result = await base.SaveChangesAsync(cancellationToken);

        if (auditEntries.Any())
        {
            AuditLogs.AddRange(auditEntries);
            await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            // Class-Level Comment
            var classComment = clrType.GetCustomAttribute<CommentAttribute>();
            if (classComment != null)
            {
                entityType.SetComment(classComment.Text);
            }

            // Property-Level Comments
            foreach (var property in clrType.GetProperties())
            {
                var propertyComment = property.GetCustomAttribute<CommentAttribute>();
                if (propertyComment != null)
                {
                    entityType.FindProperty(property.Name)?.SetComment(propertyComment.Text);
                }
            }
        }

        base.OnModelCreating(builder);
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    // public DbSet<Customer> Customers { get; set; }
    // public DbSet<Product> Products { get; set; }
    // public DbSet<StockMovement> StockMovements { get; set; }
    public DbSet<UserActionLog> UserActionLogs { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
}