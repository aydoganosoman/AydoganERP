namespace AydoganERP.Company.Application.Models;

public class EInvoiceIntegrationDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public int IntegrationType { get; set; }
    public string? IntegrationTypeName { get; set; }
    public string? Settings { get; set; }
    public bool IsActive { get; set; }
}
