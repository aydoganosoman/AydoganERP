using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Customer.Domain.Entities;

public class IBAN : Entity<Guid>
{
    public IBAN()
    {
        
    }

    private IBAN(Customer customer, string number)
    {
        this.Id = Guid.NewGuid();
        this.Customer = customer;
        this.Number = number;
    }

    public Customer Customer { get; private set; }
    public string Number { get; private set; }

    public void Update(string number) => this.Number = number;

    public static IBAN Create(Customer customer, string number)
    {
        return new IBAN(customer, number);
    }
}
