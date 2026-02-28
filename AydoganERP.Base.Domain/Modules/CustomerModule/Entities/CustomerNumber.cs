using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

public class CustomerNumber : Entity
{
    // For EF
    public CustomerNumber() { }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public int NumberType { get; private set; }
    public string Description { get; private set; }

    public static CustomerNumber Create(
        Guid id,
        Guid customerId,
        int numberType,
        string description)
    {
        return new CustomerNumber
        {
            Id = id,
            CustomerId = customerId,
            NumberType = numberType,
            Description = description
        };
    }

    public void Update(int numberType, string description)
    {
        NumberType = numberType;
        Description = description;
    }
}
