namespace AydoganERP.Company.Application.Models;

public class ECommerceIntegrationDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public int IntegrationType { get; set; }
    public string IntegrationTypeName { get; set; } = null!;
    public string StoreName { get; set; } = null!;
    public string? IntegrationUrl { get; set; }
    public string? Username { get; set; }
    public string Credentials { get; set; } = null!;
    public bool IsActive { get; set; }
    public IntegrationDefaultsDto? Defaults { get; set; }
}
