using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Domain.Common;
using AydoganERP.User.Infrastructure.Persistence;

namespace AydoganERP.User.Infrastructure.Proccessing;

public class DomainEventUnitOfWork : IDomainEventUnitOfWork
{
    private readonly IDomainEventService _domainEventService;
    private readonly ApplicationDbContext _applicationDbContext;
    public DomainEventUnitOfWork(
        IDomainEventService domainEventService,
        ApplicationDbContext applicationDbContext)
    {
        _domainEventService = domainEventService;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        await DispatchEvents();

        return await _applicationDbContext.SaveChangesAsync(cancellationToken);
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
