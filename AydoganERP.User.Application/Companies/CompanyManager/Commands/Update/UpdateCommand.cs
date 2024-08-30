using AydoganERP.User.Application.Common.Models;
using MediatR;

namespace AydoganERP.User.Application.Companies.CompanyManager.Commands.Update;

public class UpdateCommand : IRequest<CompanyDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string CommercialTitle { get; set; }
    public string Sector { get; set; }
    public string Address { get; set; }
    public int CityId { get; set; }
    public int TownId { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string TaxAdministration { get; set; }
    public string TaxNumber { get; set; }

}
