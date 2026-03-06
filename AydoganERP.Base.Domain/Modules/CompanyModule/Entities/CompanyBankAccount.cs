using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

public class CompanyBankAccount : Entity
{
    // For EF
    public CompanyBankAccount() { }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;
    public string BankName { get; private set; } = default!;
    public string? BranchName { get; private set; }
    public string? AccountNo { get; private set; }
    public string? AccountName { get; private set; }
    public string Iban { get; private set; } = default!;
    public string? SwiftCode { get; private set; }
    public int Currency { get; private set; } // 0=TRY, 1=USD, 2=EUR, 3=GBP
    public bool IsActive { get; private set; } = true;

    public static CompanyBankAccount Create(
        Guid id,
        Guid companyId,
        string bankName,
        string iban,
        int currency,
        string? branchName = null,
        string? accountNo = null,
        string? accountName = null,
        string? swiftCode = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (string.IsNullOrWhiteSpace(bankName)) throw new ArgumentException("BankName is required.");
        if (string.IsNullOrWhiteSpace(iban)) throw new ArgumentException("IBAN is required.");

        return new CompanyBankAccount
        {
            Id = id,
            CompanyId = companyId,
            BankName = bankName.Trim(),
            BranchName = branchName?.Trim(),
            AccountNo = accountNo?.Trim(),
            AccountName = accountName?.Trim(),
            Iban = iban.Trim().Replace(" ", "").ToUpperInvariant(),
            SwiftCode = swiftCode?.Trim().ToUpperInvariant(),
            Currency = currency,
            IsActive = true
        };
    }

    public void Update(
        string bankName,
        string iban,
        int currency,
        string? branchName,
        string? accountNo,
        string? accountName,
        string? swiftCode,
        bool isActive)
    {
        if (string.IsNullOrWhiteSpace(bankName)) throw new ArgumentException("BankName is required.");
        if (string.IsNullOrWhiteSpace(iban)) throw new ArgumentException("IBAN is required.");

        BankName = bankName.Trim();
        BranchName = branchName?.Trim();
        AccountNo = accountNo?.Trim();
        AccountName = accountName?.Trim();
        Iban = iban.Trim().Replace(" ", "").ToUpperInvariant();
        SwiftCode = swiftCode?.Trim().ToUpperInvariant();
        Currency = currency;
        IsActive = isActive;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
