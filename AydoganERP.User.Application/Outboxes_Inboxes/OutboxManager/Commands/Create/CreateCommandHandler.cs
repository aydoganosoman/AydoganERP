using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Outboxes_Inboxes.OutboxManager.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Guid>
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CreateCommandHandler(IOutboxRepository outboxRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _outboxRepository = outboxRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task<Guid> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        Outbox createdOutbox = Outbox.Create(request.Event,
            request.Data,
            (OutboxInboxSendTypeEnum)request.SendType);

        await _outboxRepository.AddAsync(createdOutbox, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        return createdOutbox.Id;
    }
}
