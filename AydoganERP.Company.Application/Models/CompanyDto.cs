using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

namespace AydoganERP.Company.Application.Models;

public class CompanyDto : IMapFrom<Base.Domain.Modules.CompanyModule.Entities.Company>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Status { get; set; }
    public string? StatusName { get; set; }
}
