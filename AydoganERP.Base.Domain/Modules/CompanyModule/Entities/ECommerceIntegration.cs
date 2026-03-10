using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

public class ECommerceIntegration : Entity
{
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public int IntegrationType { get; private set; }
    public string StoreName { get; private set; } = null!;
    public string? IntegrationUrl { get; private set; }
    public string? Username { get; private set; }
    public string Credentials { get; private set; } = null!;
    public bool IsActive { get; private set; }

    // Navigation
    public IntegrationDefaults? Defaults { get; private set; }

    private ECommerceIntegration() { }

    public static ECommerceIntegration Create(
        Guid id,
        Guid companyId,
        int integrationType,
        string storeName,
        string credentials,
        string? integrationUrl = null,
        string? username = null)
    {
        return new ECommerceIntegration
        {
            Id = id,
            CompanyId = companyId,
            IntegrationType = integrationType,
            StoreName = storeName,
            Credentials = credentials,
            IntegrationUrl = integrationUrl,
            Username = username,
            IsActive = true
        };
    }

    public void Update(
        string storeName,
        string credentials,
        string? integrationUrl,
        string? username,
        bool isActive)
    {
        StoreName = storeName;
        Credentials = credentials;
        IntegrationUrl = integrationUrl;
        Username = username;
        IsActive = isActive;
    }
}
