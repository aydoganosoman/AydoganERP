namespace AydoganERP.Base.Application.Interfaces;

public interface IDomainEventUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
}
