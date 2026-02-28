using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Events;

public sealed record StockMovementCreatedEvent(
    Guid MovementId,
    Guid ProductId,
    int Type,
    decimal QuantityDelta,
    string? ReferenceType,
    Guid? ReferenceId) : IDomainEvent;
