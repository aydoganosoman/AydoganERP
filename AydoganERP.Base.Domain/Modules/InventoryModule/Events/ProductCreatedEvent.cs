using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Events;

public sealed record ProductCreatedEvent(Guid ProductId, Guid CompanyId, string Code, string Name) : IDomainEvent;
