using AydoganERP.Base.Domain.Entities;

namespace AydoganERP.Base.Application.Repositories.OutboxRepository;
public interface IOutboxRepository : IAsyncRepository<Outbox, OutboxDto, Guid>, IRepository<Outbox, OutboxDto, Guid> { }