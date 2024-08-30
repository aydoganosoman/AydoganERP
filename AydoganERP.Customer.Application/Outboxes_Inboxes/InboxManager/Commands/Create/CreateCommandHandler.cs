using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.InboxRepository;
using AydoganERP.Base.Domain;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.Customer.Application.Outboxes_Inboxes.InboxManager.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Guid>
{
    private readonly IInboxRepository _inboxRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CreateCommandHandler(IInboxRepository inboxRepository,
         IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _inboxRepository = inboxRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task<Guid> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        Inbox createdInbox = Inbox.Create(request.Event,
            request.Data,
            (OutboxInboxSendTypeEnum)request.SendType,
            request.EntryId);

        await _inboxRepository.AddAsync(createdInbox, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        return createdInbox.Id;
    }
}
