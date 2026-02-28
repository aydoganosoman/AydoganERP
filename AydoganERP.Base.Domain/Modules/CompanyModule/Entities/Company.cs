using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Enums;
using AydoganERP.Base.Domain.Modules.CompanyModule.Events;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;

namespace AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

public class Company : Entity
{
    // For EF
    public Company() { }
    
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public int Status { get; private set; } = CompanyStatusEnum.Active;

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

    public void Suspend() => Status = CompanyStatusEnum.Suspended;
    public void Activate() => Status = CompanyStatusEnum.Active;
}
