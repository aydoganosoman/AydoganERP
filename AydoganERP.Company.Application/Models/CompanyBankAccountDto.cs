using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

namespace AydoganERP.Company.Application.Models;

public class CompanyBankAccountDto : IMapFrom<CompanyBankAccount>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string BankName { get; set; } = string.Empty;
    public string? BranchName { get; set; }
    public string? AccountNo { get; set; }
    public string? AccountName { get; set; }
    public string Iban { get; set; } = string.Empty;
    public string? SwiftCode { get; set; }
    public int Currency { get; set; }
    public string? CurrencyName { get; set; }
    public bool IsActive { get; set; }
}
