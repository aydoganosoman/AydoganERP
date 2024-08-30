using AydoganERP.Base.Application.Repositories;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Customer.Infrastructure.Persistence;

namespace AydoganERP.Customer.Application.Repositories.InboxRepository;
public class OutboxRepository : EfRepositoryBase<Outbox, OutboxDto, Guid, ApplicationDbContext>, IOutboxRepository
{
    public OutboxRepository(ApplicationDbContext context)
        : base(context) { }
}
