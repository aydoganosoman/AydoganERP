using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;

namespace AydoganERP.Base.Domain.Common;

public interface IEntity
{
    IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
}

/// <summary>
/// Base class for entities.
/// </summary>
public abstract class Entity<TId> : AuditableEntity, IEntity
{
    public TId Id { get; set; }

    [NotMapped]
    private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new ConcurrentQueue<IDomainEvent>();

    [NotMapped]
    public IProducerConsumerCollection<IDomainEvent> DomainEvents => _domainEvents;

    protected void PublishEvent(IDomainEvent @event)
    {
        _domainEvents.Enqueue(@event);
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
