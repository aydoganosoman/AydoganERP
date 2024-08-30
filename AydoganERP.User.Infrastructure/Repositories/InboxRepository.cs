using AydoganERP.Base.Application.Repositories;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Infrastructure.Persistence;

namespace AydoganERP.User.Infrastructure.Repositories;
public class InboxRepository : EfRepositoryBase<Inbox, InboxDto, Guid, ApplicationDbContext>, IInboxRepository
{
    public InboxRepository(ApplicationDbContext context)
        : base(context) { }
}
