using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Events.CustomerModule.Customer;

public class CustomerRegisteredEvent : IDomainEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }

    public CustomerRegisteredEvent(Guid id, string name, string shortName)
    {
        Id = id;
        Name = name;
        ShortName = shortName;
    }
}
