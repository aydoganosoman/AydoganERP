using AydoganERP.Base.Domain.Modules.SharedModule.Enums;

namespace AydoganERP.Base.Application.Common.Models.Dtos;

public class FolderDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public FolderDocumentTypeEnum DocumentTypes { get; set; }
    public List<string> DocumentTypeNames { get; set; } = new();
    public bool IsActive { get; set; }
}
