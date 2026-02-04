using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CustomerModule.Events;

public sealed record CustomerCreatedEvent(Guid CustomerId, string Code) : IDomainEvent;