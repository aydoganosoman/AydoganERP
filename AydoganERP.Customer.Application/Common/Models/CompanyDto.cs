using AydoganERP.Customer.Domain.Entities;

namespace AydoganERP.Customer.Application.Common.Models;
public class CompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static explicit operator CompanyDto(Company company)
    {
        return new CompanyDto()
        {
            Id = company.Id,
            Name = company.Name
        };
    }

}
