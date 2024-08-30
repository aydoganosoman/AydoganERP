using AydoganERP.Base.Application.Mappings;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.User.Domain.Entities;

namespace AydoganERP.User.Application.Common.Models;

public class UserDto : IMapFrom<Domain.Entities.User>
{
    public string EMail { get; set; }
    public string ApiKey { get; set; }
    public bool IsEnable { get; set; }
    public List<CompanyDto> Companies { get; set; } = new List<CompanyDto>();

}

public class CompanyDto : IMapFrom<Company>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string CommercialTitle { get; set; }
    public string Sector { get; set; }
    public string Address { get; set; }
    public City City { get; set; }
    public Town Town { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string TaxAdministration { get; set; }
    public string TaxNumber { get; set; }

    public List<CompanyUserAuthorizationDto> CompanyUserAuthorizations { get; set; } = new List<CompanyUserAuthorizationDto>();

    public static explicit operator CompanyDto(Company company)
    {
        return new CompanyDto()
        {
            Id = company.Id,
            Name = company.Name,
            Logo = company.Logo,
            CommercialTitle = company.CommercialTitle,
            Sector = company.Sector,
            Address = company.Address,
            City = company.City,
            Town = company.Town,
            Phone = company.Phone,
            Fax = company.Fax,
            TaxAdministration = company.TaxAdministration,
            TaxNumber = company.TaxNumber,
        };
    }
}

public class CompanyUserAuthorizationDto : IMapFrom<CompanyUserAuthorization>
{
    public int Module { get; set; }
    public int AuthorizationTransactionType { get; set; }
}
