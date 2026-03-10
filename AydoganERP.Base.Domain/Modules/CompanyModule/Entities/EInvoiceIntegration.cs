using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

public class EInvoiceIntegration :Entity
{
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public int IntegrationType { get; private set; }
    public string? Settings { get; private set; }
    public bool IsActive { get; private set; }
    
    private EInvoiceIntegration() { }

    public static EInvoiceIntegration Create(
        Guid id,
        Guid companyId,
        int integrationType,
        string settings)
    {
        return new EInvoiceIntegration
        {
            Id = id,
            CompanyId = companyId,
            IntegrationType = integrationType,
            Settings = settings,
            IsActive = true
        };
    }

    public void Update(string settings,
        bool isActive)
    {
        Settings = settings;
        IsActive = isActive;
    }
}