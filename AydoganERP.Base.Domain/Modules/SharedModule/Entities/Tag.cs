using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

/// <summary>
/// Etiket - Belgelere atanabilecek etiketler
/// </summary>
public class Tag : Entity
{
    // For EF
    public Tag() { }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;
    public Guid TagGroupId { get; private set; }
    public TagGroup TagGroup { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Color { get; private set; } // HTML Format (#FFFFFF)
    public bool IsActive { get; private set; } = true;

    public static Tag Create(
        Guid id,
        Guid companyId,
        Guid tagGroupId,
        string name,
        string? color = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (tagGroupId == Guid.Empty) throw new ArgumentException("TagGroupId cannot be empty.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        return new Tag
        {
            Id = id,
            CompanyId = companyId,
            TagGroupId = tagGroupId,
            Name = name.Trim(),
            Color = color?.Trim(),
            IsActive = true
        };
    }

    public void Update(string name, string? color)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        Name = name.Trim();
        Color = color?.Trim();
    }

    public void ChangeGroup(Guid tagGroupId)
    {
        if (tagGroupId == Guid.Empty) throw new ArgumentException("TagGroupId cannot be empty.");
        TagGroupId = tagGroupId;
    }

    public void SetActive(bool active) => IsActive = active;
}
