using AydoganERP.Base.Domain.Common;
using AydoganERP.Customer.Domain.Enums;

namespace AydoganERP.Customer.Domain.Entities;

public class SalesOpportunity : Entity<Guid>
{
    public SalesOpportunity()
    {
        
    }

    public SalesOpportunity(Customer customer,
        string description,
        OpportunityStatusEnum statu,
        double value,
        DateTime expectedCloseDate)
    {
        this.Id = Guid.NewGuid();
        this.Customer = customer;
        this.Description = description;
        this.Statu = statu;
        this.Value = value;
        this.ExpectedCloseDate = expectedCloseDate;
    }

    public Customer Customer { get; private set; }
    public string Description { get; private set; }
    public OpportunityStatusEnum Statu { get; private set; }
    public double Value { get; private set; }
    public DateTime ExpectedCloseDate { get; private set; }

    public void Close() => this.Statu = OpportunityStatusEnum.Close;
    public void Won() => this.Statu = OpportunityStatusEnum.Won;
    public void Lost() => this.Statu = OpportunityStatusEnum.Lost;

    public void Update(string description,
        OpportunityStatusEnum statu,
        double value,
        DateTime expectedCloseDate)
    {
        this.Description = description;
        this.Statu = statu;
        this.Value = value;
        this.ExpectedCloseDate = expectedCloseDate;
    }

    public static SalesOpportunity Create(Customer customer,
        string description,
        double value,
        DateTime expectedCloseDate)
    {
        return new SalesOpportunity(customer, description, OpportunityStatusEnum.Open, value, expectedCloseDate);
    }

}
