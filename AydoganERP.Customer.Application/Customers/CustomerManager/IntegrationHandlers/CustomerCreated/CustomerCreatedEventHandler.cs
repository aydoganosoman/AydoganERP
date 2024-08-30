using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Repositories.OutboxRepository;
using AydoganERP.Base.Domain;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Base.Domain.Events.CustomerModule.Customer;
using MediatR;
using Newtonsoft.Json;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.IntegrationHandlers.CustomerCreated;

public class CustomerCreatedEventHandler : INotificationHandler<DomainEventNotification<CustomerRegisteredEvent>>
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CustomerCreatedEventHandler(IOutboxRepository outboxRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _outboxRepository = outboxRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(DomainEventNotification<CustomerRegisteredEvent> notification, CancellationToken cancellationToken)
    {
        await _outboxRepository.AddAsync(Outbox.Create(notification.DomainEvent.GetType().FullName,
                       JsonConvert.SerializeObject(notification.DomainEvent, Formatting.Indented),
                       OutboxInboxSendTypeEnum.Redis));
    }
}
