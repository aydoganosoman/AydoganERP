using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Base.Domain.Events.UserModule.Company;
using MediatR;
using Newtonsoft.Json;

namespace AydoganERP.User.Application.Companies.CompanyManager.IntegrationHandlers.CompanyUpdated;

public class CompanyUpdatedEventHandler : INotificationHandler<DomainEventNotification<CompanyUpdatedEvent>>
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CompanyUpdatedEventHandler(IOutboxRepository outboxRepository,
         IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _outboxRepository = outboxRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(DomainEventNotification<CompanyUpdatedEvent> notification, CancellationToken cancellationToken)
    {
        await _outboxRepository.AddAsync(Outbox.Create(notification.DomainEvent.GetType().AssemblyQualifiedName,
            JsonConvert.SerializeObject(notification.DomainEvent, Formatting.Indented),
            OutboxInboxSendTypeEnum.Redis));
    }
}
