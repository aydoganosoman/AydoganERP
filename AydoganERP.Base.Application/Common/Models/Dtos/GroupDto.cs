using AydoganERP.Base.Domain.Modules.SharedModule.Enums;

namespace AydoganERP.Base.Application.Common.Models.Dtos;

public class GroupDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public UsageAreaEnum UsageAreas { get; set; }
    public List<string> UsageAreaNames { get; set; } = new();
    public bool IsActive { get; set; }
}
