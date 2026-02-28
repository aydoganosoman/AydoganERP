using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;

namespace AydoganERP.Customer.Application.Models;

public class CustomerDto : IMapFrom<Base.Domain.Modules.CustomerModule.Entities.Customer>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? SurName { get; set; }
    public int Type { get; set; }
    public int PartyType { get; set; }
    public int Status { get; set; }
    public TaxInfo TaxInfo { get; set; } = TaxInfo.Empty;
    public ContactInfo Contact { get; set; } = ContactInfo.Empty;
    public Address? Address { get; set; } = Address.Empty;  
    public List<CustomerBankAccountDto> BankAccounts { get; set; }
    public List<CustomerBranchDto> Branches { get; set; }
    public List<CustomerContactDto> Contacts { get; set; }
    public List<CustomerNumberDto> Numbers { get; set; } 
    public List<CustomerNoteDto> Notes { get; set; } 
}
