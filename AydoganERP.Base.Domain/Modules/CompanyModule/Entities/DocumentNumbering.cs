using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

public class DocumentNumbering : Entity
{
    // For EF
    public DocumentNumbering() { }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;
    public int DocumentType { get; private set; } // 0=E-Fatura, 1=E-Arşiv, 2=E-İrsaliye, vb.
    public string Prefix { get; private set; } = default!; // Ön Ek (AYD, INT vb.)
    public int CurrentNumber { get; private set; } = 1;
    public bool IsDefault { get; private set; }
    public bool IsActive { get; private set; } = true;

    public static DocumentNumbering Create(
        Guid id,
        Guid companyId,
        int documentType,
        string prefix,
        bool isDefault = false)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (string.IsNullOrWhiteSpace(prefix)) throw new ArgumentException("Prefix is required.");

        return new DocumentNumbering
        {
            Id = id,
            CompanyId = companyId,
            DocumentType = documentType,
            Prefix = prefix.Trim().ToUpperInvariant(),
            CurrentNumber = 1,
            IsDefault = isDefault,
            IsActive = true
        };
    }

    public void Update(string prefix, bool isDefault, bool isActive)
    {
        if (string.IsNullOrWhiteSpace(prefix)) throw new ArgumentException("Prefix is required.");
        
        Prefix = prefix.Trim().ToUpperInvariant();
        IsDefault = isDefault;
        IsActive = isActive;
    }

    public string GetNextNumber()
    {
        var number = $"{Prefix}{CurrentNumber:D9}";
        CurrentNumber++;
        return number;
    }

    public void SetAsDefault() => IsDefault = true;
    public void UnsetDefault() => IsDefault = false;
    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
