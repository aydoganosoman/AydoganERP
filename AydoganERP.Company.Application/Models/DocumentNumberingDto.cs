using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

namespace AydoganERP.Company.Application.Models;

public class DocumentNumberingDto : IMapFrom<DocumentNumbering>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public int DocumentType { get; set; }
    public string? DocumentTypeName { get; set; }
    public string Prefix { get; set; } = string.Empty;
    public int CurrentNumber { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
}
