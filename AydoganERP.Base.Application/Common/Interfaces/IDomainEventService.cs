using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(IDomainEvent domainEvent);
}
