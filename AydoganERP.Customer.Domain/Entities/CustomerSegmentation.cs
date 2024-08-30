using AydoganERP.Base.Domain.Common;
using AydoganERP.Customer.Domain.Enums;

namespace AydoganERP.Customer.Domain.Entities;

public class CustomerSegmentation : Entity<Guid>
{
    public CustomerSegmentation()
    {
        
    }

    public CustomerSegmentation(string name,
        string description,
        double discountRate,
        RiskLevelEnumType riskLevel)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Description = description;
        this.DiscountRate = discountRate;
        this.RiskLevel = riskLevel;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public double DiscountRate { get; private set; }
    public RiskLevelEnumType RiskLevel { get; private set; }

    public void Update(string name,
        string description,
        double discountRate,
        RiskLevelEnumType riskLevel)
    {
        this.Name = name;
        this.Description = description;
        this.DiscountRate = discountRate;
        this.RiskLevel = riskLevel;
    }

    public static CustomerSegmentation Create(string name,
        string description,
        double discountRate,
        RiskLevelEnumType riskLevel)
    {
        return new CustomerSegmentation(name, description, discountRate, riskLevel);

    }
}
