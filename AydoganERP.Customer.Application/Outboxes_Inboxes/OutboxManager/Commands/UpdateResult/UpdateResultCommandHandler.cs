using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.Customer.Application.Outboxes_Inboxes.OutboxManager.Commands.UpdateResult;

public class UpdateResultCommandHandler : IRequestHandler<UpdateResultCommand>
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public UpdateResultCommandHandler(IOutboxRepository outboxRepository,
         IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _outboxRepository = outboxRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(UpdateResultCommand request, CancellationToken cancellationToken)
    {
        Outbox updatedOutbox = await _outboxRepository
            .GetAsync(x => x.Id == request.Id);

        updatedOutbox?.SetDone(request.IsDone);
        updatedOutbox?.SetResult(request.Result);

        await _outboxRepository.UpdateAsync(updatedOutbox);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);
    }
}
