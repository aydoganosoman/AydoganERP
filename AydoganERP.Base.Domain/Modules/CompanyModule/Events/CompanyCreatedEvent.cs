using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CompanyModule.Events;

public sealed record CompanyCreatedEvent(Guid CompanyId, string Name) : IDomainEvent;
