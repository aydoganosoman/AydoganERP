using AydoganERP.Base.Domain.Common;
using MediatR;

namespace AydoganERP.Base.Application.Common.Models;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
{
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}
