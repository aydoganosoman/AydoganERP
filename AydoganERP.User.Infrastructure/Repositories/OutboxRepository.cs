using AydoganERP.Base.Application.Repositories;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Infrastructure.Persistence;

namespace AydoganERP.User.Infrastructure.Repositories;
public class OutboxRepository : EfRepositoryBase<Outbox, OutboxDto, Guid, ApplicationDbContext>, IOutboxRepository
{
    public OutboxRepository(ApplicationDbContext context)
        : base(context) { }
}
