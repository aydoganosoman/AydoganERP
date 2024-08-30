using MediatR;

namespace AydoganERP.User.Application.Outboxes_Inboxes.OutboxManager.Commands.Create;

public class CreateCommand : IRequest<Guid>
{
    public string Event { get; set; }
    public string Data { get; set; }
    public int SendType { get; set; }
}
