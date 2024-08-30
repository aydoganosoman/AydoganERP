using AydoganERP.Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.Persistence;
public abstract class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<Inbox> Inboxes { get; set; }
    public DbSet<Outbox> Outboxes { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Town> Towns { get; set; }
    public DbSet<Category> Categories { get; set; }

}
