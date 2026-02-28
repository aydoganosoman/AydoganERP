using AydoganERP.Base.Application.Common.Models;
using AydoganERP.Base.Domain.Modules.InventoryModule.Enums;
using AydoganERP.Base.Domain.Modules.InventoryModule.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AydoganERP.Inventory.Application.StockMovementManager.EventHandlers;

public class StockMovementCreatedEventHandler : INotificationHandler<DomainEventNotification<StockMovementCreatedEvent>>
{
    private readonly ILogger<StockMovementCreatedEventHandler> _logger;

    public StockMovementCreatedEventHandler(ILogger<StockMovementCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<StockMovementCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation(
            "Stock movement created: {MovementId}, Product: {ProductId}, Type: {Type} ({TypeName}), Delta: {QuantityDelta}",
            domainEvent.MovementId,
            domainEvent.ProductId,
            domainEvent.Type,
            StockMovementTypeEnum.GetName(domainEvent.Type),
            domainEvent.QuantityDelta);

        // Burada ek işlemler yapılabilir:
        // - Stok seviyesi kritik eşiğin altına düştüyse uyarı
        // - E-ticaret platformlarına stok güncelleme
        // - Raporlama sistemine bildirim

        return Task.CompletedTask;
    }
}
