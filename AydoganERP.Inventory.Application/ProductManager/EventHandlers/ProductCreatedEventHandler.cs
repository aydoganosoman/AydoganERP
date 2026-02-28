using AydoganERP.Base.Application.Common.Models;
using AydoganERP.Base.Domain.Modules.InventoryModule.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AydoganERP.Inventory.Application.ProductManager.EventHandlers;

public class ProductCreatedEventHandler : INotificationHandler<DomainEventNotification<ProductCreatedEvent>>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<ProductCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation(
            "Product created: {ProductId}, Company: {CompanyId}, Code: {Code}, Name: {Name}",
            domainEvent.ProductId,
            domainEvent.CompanyId,
            domainEvent.Code,
            domainEvent.Name);

        // Burada ek işlemler yapılabilir:
        // - E-ticaret entegrasyonu
        // - Stok takip sistemi bildirimi
        // - Cache güncelleme

        return Task.CompletedTask;
    }
}
