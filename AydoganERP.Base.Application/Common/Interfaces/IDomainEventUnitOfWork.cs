namespace AydoganERP.Base.Application.Common.Interfaces;

public interface IDomainEventUnitOfWork
{
    Task<int> CommitAsync(IBaseDbContext applicationDbContext = null, CancellationToken cancellationToken = default(CancellationToken));
}
