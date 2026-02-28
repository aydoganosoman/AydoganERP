using AydoganERP.Base.Application.Common.Models;
using AydoganERP.Base.Domain.Modules.CustomerModule.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AydoganERP.Customer.Application.CustomerManager.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<DomainEventNotification<CustomerCreatedEvent>>
{
    private readonly ILogger<CustomerCreatedEventHandler> _logger;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<CustomerCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation(
            "Customer created: {CustomerId}, Code: {Code}",
            domainEvent.CustomerId,
            domainEvent.Code);

        // Burada ek işlemler yapılabilir:
        // - Bildirim gönderme
        // - Cache güncelleme
        // - Entegrasyon eventleri publish etme

        return Task.CompletedTask;
    }
}
