using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

/// <summary>
/// Grup - Kategorilerin üst gruplaması
/// </summary>
public class Group : Entity
{
    // For EF
    public Group() { }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public UsageAreaEnum UsageAreas { get; private set; }
    public bool IsActive { get; private set; } = true;

    public List<Category> Categories { get; private set; } = new();

    public static Group Create(
        Guid id,
        Guid companyId,
        string code,
        string name,
        UsageAreaEnum usageAreas)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code is required.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        return new Group
        {
            Id = id,
            CompanyId = companyId,
            Code = code.Trim(),
            Name = name.Trim(),
            UsageAreas = usageAreas,
            IsActive = true
        };
    }

    public void Update(string name, UsageAreaEnum usageAreas)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        Name = name.Trim();
        UsageAreas = usageAreas;
    }

    public void SetActive(bool active) => IsActive = active;
}
