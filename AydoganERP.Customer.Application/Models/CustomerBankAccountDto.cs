using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

namespace AydoganERP.Customer.Application.Models;

public class CustomerBankAccountDto : IMapFrom<CustomerBankAccount>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public int SortOrder { get; set; }
    public string IBAN { get; set; }
    public int CurrencyType { get; set; }
    public string BankName { get; set; }
}