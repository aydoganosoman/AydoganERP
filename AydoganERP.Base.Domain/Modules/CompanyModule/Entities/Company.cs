using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Enums;
using AydoganERP.Base.Domain.Modules.CompanyModule.Events;
using AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;

namespace AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

public class Company : Entity
{
    // For EF
    public Company() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public int Status { get; private set; } = CompanyStatusEnum.Active;

    // Firma Bilgileri
    public int CompanyType { get; private set; } = CompanyTypeEnum.Corporate; // Tüzel/Gerçek Kişi
    public string? ShortName { get; private set; }
    public string? TradeRegisterNo { get; private set; } // Sicil No
    public string? TradeRegisterTitle { get; private set; } // Ticaret Sicil Unvanı
    public string? MersisNo { get; private set; }
    public string? TapdkNo { get; private set; }
    public string? HeadquartersAddress { get; private set; } // İşletme Merkezi
    public int Currency { get; private set; } = 0; // 0=TRY, 1=USD, 2=EUR
    public decimal Capital { get; private set; } = 0; // Sermaye Tutarı
    public DateTime? EstablishmentDate { get; private set; } // Firma Açılış Tarihi

    // ValueObjects
    public TaxInfo TaxInfo { get; private set; } = TaxInfo.Empty;
    public ContactInfo Contact { get; private set; } = ContactInfo.Empty;
    public Address? Address { get; private set; } = Address.Empty;

    public List<User> Users { get; private set; } = new();

    public static Company Create(Guid id, string name)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        var company = new Company
        {
            Id = id,
            Name = name.Trim(),
            Status = CompanyStatusEnum.Active,
        };

        company.PublishEvent(new CompanyCreatedEvent(company.Id, company.Name));

        return company;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        Name = name.Trim();
    }

    public void UpdateDetails(
        int companyType,
        string? shortName,
        string? tradeRegisterNo,
        string? tradeRegisterTitle,
        string? mersisNo,
        string? tapdkNo,
        string? headquartersAddress,
        int currency,
        decimal capital,
        DateTime? establishmentDate,
        TaxInfo? taxInfo,
        ContactInfo? contact,
        Address? address)
    {
        CompanyType = companyType;
        ShortName = shortName?.Trim();
        TradeRegisterNo = tradeRegisterNo?.Trim();
        TradeRegisterTitle = tradeRegisterTitle?.Trim();
        MersisNo = mersisNo?.Trim();
        TapdkNo = tapdkNo?.Trim();
        HeadquartersAddress = headquartersAddress?.Trim();
        Currency = currency;
        Capital = capital;
        EstablishmentDate = establishmentDate;
        TaxInfo = taxInfo ?? TaxInfo.Empty;
        Contact = contact ?? ContactInfo.Empty;
        Address = address;
    }

    public void Suspend() => Status = CompanyStatusEnum.Suspended;
    public void Activate() => Status = CompanyStatusEnum.Active;
}
