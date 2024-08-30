using AydoganERP.Base.Domain.Entities;

namespace AydoganERP.Base.Application.Repositories.InboxRepository;
public interface IInboxRepository : IAsyncRepository<Inbox, InboxDto, Guid>, IRepository<Inbox, InboxDto, Guid> { }