using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;

namespace AydoganERP.Customer.Application.Models;

public class CustomerBranchDto : IMapFrom<CustomerBranch>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ContactInfo Contact { get; set; } = ContactInfo.Empty;
    public Address? Address { get; set; }
}
