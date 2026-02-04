using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Infrastructure.Persistence;

namespace AydoganERP.Base.Infrastructure.Proccessing;

public class DomainEventUnitOfWork : IDomainEventUnitOfWork
{
    private readonly IDomainEventService _domainEventService;
    private readonly IBaseDbContext _applicationDbContext;

    public DomainEventUnitOfWork(
        IDomainEventService domainEventService,
        ApplicationDbContext applicationDbContext)
    {
        _domainEventService = domainEventService;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> CommitAsync(IBaseDbContext applicationDbContext = null, CancellationToken cancellationToken = default)
    {
        if (applicationDbContext == null)
        {
            await DispatchEvents();
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        else 
            return await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task DispatchEvents()
    {
        var domainEventEntities = _applicationDbContext.ChangeTracker
            .Entries<IEntity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            IDomainEvent dev;
            while (entity.DomainEvents.TryTake(out dev))
            {
                await _domainEventService.Publish(dev);
            }
        }
    }
}