using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Outboxes_Inboxes.InboxManager.Commands.UpdateResult;

public class UpdateResultCommandHandler : IRequestHandler<UpdateResultCommand>
{
    private readonly IInboxRepository _inboxRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public UpdateResultCommandHandler(IInboxRepository inboxRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _inboxRepository = inboxRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(UpdateResultCommand request, CancellationToken cancellationToken)
    {
        Inbox updatedInbox = await _inboxRepository
            .GetAsync(x => x.Id == request.Id);

        updatedInbox.SetDone(request.IsDone);
        updatedInbox.SetResult(request.Result);

        await _inboxRepository.UpdateAsync(updatedInbox);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);
    }
}
