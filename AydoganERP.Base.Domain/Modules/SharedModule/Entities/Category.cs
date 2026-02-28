using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

/// <summary>
/// Kategori - Gruba bağlı alt kategoriler
/// </summary>
public class Category : Entity
{
    // For EF
    public Category() { }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;
    public Guid GroupId { get; private set; }
    public Group Group { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Color { get; private set; } // HTML Format (#FFFFFF)
    public ProcessTypeEnum ProcessTypes { get; private set; }
    public bool IsActive { get; private set; } = true;

    public static Category Create(
        Guid id,
        Guid companyId,
        Guid groupId,
        string code,
        string name,
        ProcessTypeEnum processTypes,
        string? color = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (groupId == Guid.Empty) throw new ArgumentException("GroupId cannot be empty.");
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code is required.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        return new Category
        {
            Id = id,
            CompanyId = companyId,
            GroupId = groupId,
            Code = code.Trim(),
            Name = name.Trim(),
            Color = color?.Trim(),
            ProcessTypes = processTypes,
            IsActive = true
        };
    }

    public void Update(string name, ProcessTypeEnum processTypes, string? color)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        Name = name.Trim();
        ProcessTypes = processTypes;
        Color = color?.Trim();
    }

    public void ChangeGroup(Guid groupId)
    {
        if (groupId == Guid.Empty) throw new ArgumentException("GroupId cannot be empty.");
        GroupId = groupId;
    }

    public void SetActive(bool active) => IsActive = active;
}
