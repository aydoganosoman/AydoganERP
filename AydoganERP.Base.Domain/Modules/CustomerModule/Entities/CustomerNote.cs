using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

public class CustomerNote : Entity
{
    // For EF
    public CustomerNote() { }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public DateTime Date { get; private set; }
    public string Note { get; private set; }

    public static CustomerNote Create(
        Guid id,
        Guid customerId,
        DateTime date,
        string note)
    {
        return new CustomerNote
        {
            Id = id,
            CustomerId = customerId,
            Date = date,
            Note = note
        };
    }

    public void Update(DateTime date, string note)
    {
        Date = date;
        Note = note;
    }
}
