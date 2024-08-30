using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Events.UserModule.Company;

public class CompanyCreatedEvent : IDomainEvent
{
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }

    public CompanyCreatedEvent(Guid companyId, string companyName)
    {
        CompanyId = companyId;
        CompanyName = companyName;
    }
}
