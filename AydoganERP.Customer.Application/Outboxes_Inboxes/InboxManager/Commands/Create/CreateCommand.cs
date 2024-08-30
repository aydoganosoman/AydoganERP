using AydoganERP.Base.Domain.ValueObjects;
using MediatR;

namespace AydoganERP.Customer.Application.Outboxes_Inboxes.InboxManager.Commands.Create;

public class CreateCommand : IRequest<Guid>
{
    public string Event { get; set; }
    public string Data { get; set; }
    public int SendType { get; set; }
    public RedisEntryId EntryId { get; set; }
}
