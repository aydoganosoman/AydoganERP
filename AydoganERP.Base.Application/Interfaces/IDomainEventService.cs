using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Application.Interfaces;

public interface IDomainEventService
{
    Task Publish(IDomainEvent domainEvent);
}
