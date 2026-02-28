using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

/// <summary>
/// Etiket Grubu - Etiketlerin gruplandırılması
/// </summary>
public class TagGroup : Entity
{
    // For EF
    public TagGroup() { }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;

    public List<Tag> Tags { get; private set; } = new();

    public static TagGroup Create(Guid id, Guid companyId, string name)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        return new TagGroup
        {
            Id = id,
            CompanyId = companyId,
            Name = name.Trim(),
            IsActive = true
        };
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        Name = name.Trim();
    }

    public void SetActive(bool active) => IsActive = active;
}
