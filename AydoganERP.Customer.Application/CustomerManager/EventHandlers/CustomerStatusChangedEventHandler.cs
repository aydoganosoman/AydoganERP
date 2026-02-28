using AydoganERP.Base.Application.Common.Models;
using AydoganERP.Base.Domain.Modules.CustomerModule.Enums;
using AydoganERP.Base.Domain.Modules.CustomerModule.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AydoganERP.Customer.Application.CustomerManager.EventHandlers;

public class CustomerStatusChangedEventHandler : INotificationHandler<DomainEventNotification<CustomerStatusChangedEvent>>
{
    private readonly ILogger<CustomerStatusChangedEventHandler> _logger;

    public CustomerStatusChangedEventHandler(ILogger<CustomerStatusChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<CustomerStatusChangedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation(
            "Customer status changed: {CustomerId}, From: {OldStatus} ({OldStatusName}) To: {NewStatus} ({NewStatusName})",
            domainEvent.CustomerId,
            domainEvent.OldStatus,
            CustomerStatusEnum.GetName(domainEvent.OldStatus),
            domainEvent.NewStatus,
            CustomerStatusEnum.GetName(domainEvent.NewStatus));

        // Burada ek işlemler yapılabilir:
        // - Blocked durumuna geçişte ilgili siparişleri iptal etme
        // - Passive durumuna geçişte bildirim gönderme
        // - Audit log oluşturma

        return Task.CompletedTask;
    }
}
