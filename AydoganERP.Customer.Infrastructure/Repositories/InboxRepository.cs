using AydoganERP.Base.Application.Repositories;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Customer.Infrastructure.Persistence;

namespace AydoganERP.Customer.Application.Repositories.InboxRepository;
public class InboxRepository : EfRepositoryBase<Inbox, InboxDto, Guid, ApplicationDbContext>, IInboxRepository
{
    public InboxRepository(ApplicationDbContext context)
        : base(context) { }
}
