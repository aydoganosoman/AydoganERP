using MediatR;

namespace AydoganERP.User.Application.Outboxes_Inboxes.InboxManager.Commands.UpdateResult;

public class UpdateResultCommand : IRequest
{
    public Guid Id { get; set; }
    public bool IsDone { get; set; }
    public string Result { get; set; }
}
