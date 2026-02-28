using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

/// <summary>
/// Klasör - Belgelerin organize edilmesi için
/// </summary>
public class Folder : Entity
{
    // For EF
    public Folder() { }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Color { get; private set; } // HTML Format (#FFFFFF)
    public DocumentTypeEnum DocumentTypes { get; private set; }
    public bool IsActive { get; private set; } = true;

    public static Folder Create(
        Guid id,
        Guid companyId,
        string code,
        string name,
        DocumentTypeEnum documentTypes,
        string? color = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code is required.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        return new Folder
        {
            Id = id,
            CompanyId = companyId,
            Code = code.Trim(),
            Name = name.Trim(),
            Color = color?.Trim(),
            DocumentTypes = documentTypes,
            IsActive = true
        };
    }

    public void Update(string name, DocumentTypeEnum documentTypes, string? color)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        Name = name.Trim();
        DocumentTypes = documentTypes;
        Color = color?.Trim();
    }

    public void SetActive(bool active) => IsActive = active;

    /// <summary>
    /// Belge tipinin bu klasöre ait olup olmadığını kontrol eder
    /// </summary>
    public bool AcceptsDocumentType(DocumentTypeEnum documentType) 
        => DocumentTypes.HasFlag(documentType);
}
