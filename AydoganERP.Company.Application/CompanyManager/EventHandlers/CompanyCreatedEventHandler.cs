using AydoganERP.Base.Application.Common.Models;
using AydoganERP.Base.Domain.Modules.CompanyModule.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AydoganERP.Company.Application.CompanyManager.EventHandlers;

public class CompanyCreatedEventHandler : INotificationHandler<DomainEventNotification<CompanyCreatedEvent>>
{
    private readonly ILogger<CompanyCreatedEventHandler> _logger;

    public CompanyCreatedEventHandler(ILogger<CompanyCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<CompanyCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation(
            "Company created: {CompanyId}, Name: {Name}",
            domainEvent.CompanyId,
            domainEvent.Name);

        // Burada ek işlemler yapılabilir:
        // - Varsayılan ayarların oluşturulması
        // - Hoş geldin e-postası gönderme
        // - Entegrasyon bildirimleri

        return Task.CompletedTask;
    }
}
