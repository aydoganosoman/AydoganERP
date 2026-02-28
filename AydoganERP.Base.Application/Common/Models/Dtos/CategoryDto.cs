using AydoganERP.Base.Domain.Modules.SharedModule.Enums;

namespace AydoganERP.Base.Application.Common.Models.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid GroupId { get; set; }
    public string? GroupName { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public ProcessTypeEnum ProcessTypes { get; set; }
    public List<string> ProcessTypeNames { get; set; } = new();
    public bool IsActive { get; set; }
}
