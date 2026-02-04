using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CustomerModule.Events;

public sealed record CustomerStatusChangedEvent(Guid CustomerId, int OldStatus, int NewStatus) : IDomainEvent;