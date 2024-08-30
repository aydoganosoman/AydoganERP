using MediatR;

namespace AydoganERP.Customer.Application.Outboxes_Inboxes.OutboxManager.Commands.UpdateResult;

public class UpdateResultCommand : IRequest
{
    public Guid Id { get; set; }
    public bool IsDone { get; set; }
    public string Result { get; set; }
}
