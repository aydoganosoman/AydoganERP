using AydoganERP.Base.Application.Mappings;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Domain.Entities;

namespace AydoganERP.Customer.Application.Common.Models;
public class CustomerDto : IMapFrom<Category>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ShortName { get; set; }
    public CustomerSegmentationDto? Segmentation { get; set; }
    public CategoryDto? Category { get; set; }
    public string? Phone { get; set; }
    public string? EMail { get; set; }
    public string? Fax { get; set; }
    public int? City { get; set; }
    public int? Town { get; set; }
    public string? Address { get; set; }
    public int? Type { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }
    public int? ExchangeRateType { get; set; }
    public string? EInvoice { get; set; }
    public CompanyDto Company { get; set; }

}
