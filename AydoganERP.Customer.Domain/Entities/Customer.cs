using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Base.Domain.Events.CustomerModule.Customer;
using AydoganERP.Customer.Domain.Enums;

namespace AydoganERP.Customer.Domain.Entities;

public class Customer : Entity<Guid>
{
    public Customer()
    {

    }

    private Customer(Company company, string name)
    {
        this.Company = company;
        this.Name = name;

        this.PublishEvent(new CustomerRegisteredEvent(this.Id, this.Name, this.ShortName));
    }

    private Customer(Company company,
        string name,
        string shortName,
        CustomerSegmentation segmentation,
        Category category,
        string phone,
        string email,
        string fax,
        City city,
        Town town,
        string address,
        CustomerTypeEnum type,
        string taxOffice,
        string taxNumber,
        ExchangeRateTypeEnum exchangeRateType,
        string eInvoice)
    {
        this.Id = Guid.NewGuid();
        this.Company = company;
        this.Name = name;
        this.ShortName = shortName;
        this.Segmentation = segmentation;
        this.Category = category;
        this.Phone = phone;
        this.EMail = email;
        this.Fax = fax;
        this.City = city;
        this.Town = town;
        this.Address = address;
        this.Type = type;
        this.TaxOffice = taxOffice;
        this.TaxNumber = taxNumber;
        this.ExchangeRateType = exchangeRateType;
        this.EInvoice = eInvoice;

        this.PublishEvent(new CustomerRegisteredEvent(this.Id, this.Name, this.ShortName));
    }

    public string Name { get; private set; }
    public string? ShortName { get; private set; }
    public CustomerSegmentation? Segmentation { get; set; }
    public Category? Category { get; private set; }
    public string? Phone { get; private set; }
    public string? EMail { get; private set; }
    public string? Fax { get; private set; }
    public City? City { get; private set; }
    public Town? Town { get; private set; }
    public string? Address { get; private set; }
    public CustomerTypeEnum? Type { get; private set; }
    public string? TaxOffice { get; private set; }
    public string? TaxNumber { get; private set; }
    public ExchangeRateTypeEnum? ExchangeRateType { get; private set; }
    public string? EInvoice { get; private set; }
    public Company Company { get; private set; }

    public virtual List<IBAN> IBANs { get; private set; } = new List<IBAN>();
    public virtual List<AuthorizePerson> AuthorizePeople { get; private set; } = new List<AuthorizePerson>();

    public static Customer Create(Company company, string name)
    {
        return new Customer(company, name);
    }

    public static Customer Create(Company company,
        string name,
        string shortName,
        CustomerSegmentation segmentation,
        Category category,
        string phone,
        string email,
        string fax,
        City city,
        Town town,
        string address,
        CustomerTypeEnum type,
        string taxOffice,
        string taxNumber,
        ExchangeRateTypeEnum exchangeRateType,
        string eInvoice)
    {
        return new Customer(company,
            name,
            shortName,
            segmentation,
            category,
            phone,
            email,
            fax,
            city,
            town,
            address,
            type,
            taxOffice,
            taxNumber,
            exchangeRateType,
            eInvoice);
    }

    public void AddIBAN(IBAN iban)
    {
        this.IBANs.Add(iban);
    }

    public void AddAuthorizePerson(AuthorizePerson person)
    {
        this.AuthorizePeople.Add(person);
    }

    public void Update(string name,
        string shortName,
        CustomerSegmentation segmentation,
        Category category,
        string phone,
        string email,
        string fax,
        City city,
        Town town,
        string address,
        CustomerTypeEnum type,
        string taxOffice,
        string taxNumber,
        ExchangeRateTypeEnum exchangeRateType,
        string eInvoice)
    {
        this.Name = name;
        this.ShortName = shortName;
        this.Segmentation = segmentation;
        this.Category = category;
        this.Phone = phone;
        this.EMail = email;
        this.Fax = fax;
        this.City = city;
        this.Town = town;
        this.Address = address;
        this.Type = type;
        this.TaxOffice = taxOffice;
        this.TaxNumber = taxNumber;
        this.ExchangeRateType = exchangeRateType;
        this.EInvoice = eInvoice;

        this.PublishEvent(new CustomerUpdatedEvent(this.Id, this.Name, this.ShortName));
    }

    public void UpdateIBAN(Guid id, string number)
    {
        this.IBANs
            .First(x => x.Id == id)
            .Update(number);
    }

    public void UpdateAuthorizePerson(Guid id,
        string name,
        string phone,
        string email,
        string notes)
    {
        this.AuthorizePeople
            .First(x => x.Id == id)
            .Update(name, phone, email, notes);
    }
}
