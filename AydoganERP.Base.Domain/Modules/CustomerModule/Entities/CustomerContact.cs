using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

public class CustomerContact : Entity
{
    //For EF
    public CustomerContact() { }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Title { get; private set; }
    public string GSM { get; private set; }
    public string Email { get; private set; }

    public static CustomerContact Create(
        Guid id,
        Guid customerId,
        string name,
        string surname,
        string? title,
        string? gsm,
        string? email)
    {
        return new CustomerContact
        {
            Id = id,
            CustomerId = customerId,
            Name = name,
            Surname = surname,
            Title = title ?? string.Empty,
            GSM = gsm ?? string.Empty,
            Email = email ?? string.Empty
        };
    }

    public void Update(string name, string surname, string? title, string? gsm, string? email)
    {
        Name = name;
        Surname = surname;
        Title = title ?? string.Empty;
        GSM = gsm ?? string.Empty;
        Email = email ?? string.Empty;
    }
}
