using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Customer.Domain.Entities;

public class AuthorizePerson : Entity<Guid>
{
    public AuthorizePerson()
    {
        
    }

    private AuthorizePerson(Customer customer, string name, string phone, string email, string notes)
    {
        this.Id = Guid.NewGuid();
        this.Customer = customer;
        this.Name = name;
        this.Phone = phone;
        this.EMail = email;
        this.Notes = notes;
    }

    public Customer Customer { get; private set; }
    public string Name { get; private set; }
    public string? Phone { get; private set; }
    public string? EMail { get; private set; }
    public string? Notes { get; private set; }

    public void Update(string name, string phone, string email, string notes)
    {
        this.Name = name;
        this.Phone = phone;
        this.EMail = email;
        this.Notes = notes;
    }

    public static AuthorizePerson Create(Customer customer, string name, string phone, string email, string notes)
    {
        return new AuthorizePerson(customer, name, phone, email, notes);
    }

}
