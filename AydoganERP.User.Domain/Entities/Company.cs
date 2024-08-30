using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Base.Domain.Events.UserModule.Company;

namespace AydoganERP.User.Domain.Entities;

public class Company : Entity<Guid>
{
    //For EF
    public Company()
    {

    }

    private Company(string name)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;

        this.PublishEvent(new CompanyCreatedEvent(this.Id, this.Name));
    }

    private Company(Guid id,
        string name,
        string logo,
        string commericalTitle,
        string sector,
        string address,
        City city,
        Town town,
        string phone,
        string fax,
        string taxAdministration,
        string taxNumber)
    {
        this.Id = id;
        this.Name = name;
        this.Logo = logo;
        this.CommercialTitle = commericalTitle;
        this.Sector = sector;
        this.Address = address;
        this.City = city;
        this.Town = town;
        this.Phone = phone;
        this.Fax = fax;
        this.TaxAdministration = taxAdministration;
        this.TaxNumber = taxNumber;

    }

    public string Name { get; private set; }
    public string? Logo { get; private set; }
    public string? CommercialTitle { get; private set; }
    public string? Sector { get; private set; }
    public string? Address { get; private set; }
    public City? City { get; private set; }
    public Town? Town { get; private set; }
    public string? Phone { get; private set; }
    public string? Fax { get; private set; }
    public string? TaxAdministration { get; private set; }
    public string? TaxNumber { get; private set; }

    public virtual List<UserToRole> AccessibleUser { get; private set; } = new List<UserToRole>() { };

    public static Company Create(string name)
    {
        return new Company(name);
    }

    public void Update(string name,
        string logo,
        string commericalTitle,
        string sector,
        string address,
        City city,
        Town town,
        string phone,
        string fax,
        string taxAdministration,
        string taxNumber)
    {
        this.Name = name;
        this.Logo = logo;
        this.CommercialTitle = commericalTitle;
        this.Sector = sector;
        this.Address = address;
        this.City = city;
        this.Town = town;
        this.Phone = phone;
        this.Fax = fax;
        this.TaxAdministration = taxAdministration;
        this.TaxNumber = taxNumber;

        this.PublishEvent(new CompanyUpdatedEvent(this.Id, this.Name));
    }

    public void AddAccessibleUser(UserToRole userRole)
    {
        AccessibleUser.Add(userRole);
    }
}
