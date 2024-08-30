using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Events.UserModule.Company;

public class CompanyUpdatedEvent : IDomainEvent
{
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }

    public CompanyUpdatedEvent(Guid companyId, string companyName)
    {
        CompanyId = companyId;
        CompanyName = companyName;
    }
}
