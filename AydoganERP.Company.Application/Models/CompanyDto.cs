using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

namespace AydoganERP.Company.Application.Models;

public class CompanyDto : IMapFrom<Base.Domain.Modules.CompanyModule.Entities.Company>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Status { get; set; }
    public string? StatusName { get; set; }

    // Firma Bilgileri
    public int CompanyType { get; set; }
    public string? CompanyTypeName { get; set; }
    public string? ShortName { get; set; }
    public string? TaxNumber { get; set; }
    public string? TaxOffice { get; set; }
    public string? TradeRegisterNo { get; set; }
    public string? TradeRegisterTitle { get; set; }
    public string? MersisNo { get; set; }
    public string? TapdkNo { get; set; }
    public string? HeadquartersAddress { get; set; }
    public int Currency { get; set; }
    public decimal Capital { get; set; }
    public DateTime? EstablishmentDate { get; set; }

    // İletişim Bilgileri
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }

    // Adres Bilgileri
    public int? CountryId { get; set; }
    public int? CityId { get; set; }
    public int? DistrictId { get; set; }
    public string? AddressLine { get; set; }
}
