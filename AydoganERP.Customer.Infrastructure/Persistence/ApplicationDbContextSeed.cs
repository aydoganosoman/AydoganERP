using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Persistence;

namespace AydoganERP.Customer.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    public static async Task SeedDefaultValuesAsync(IDomainEventUnitOfWork domainEventUnitOfWork, ApplicationDbContext context)
    {
        await BaseDbContextSeed.BaseSeedDefaultValuesAsync(domainEventUnitOfWork, context);

        await context.SaveChangesAsync();
    }
}
