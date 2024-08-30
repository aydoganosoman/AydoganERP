using AydoganERP.Base.Domain.Common;
using AydoganERP.Customer.Domain.Enums;

namespace AydoganERP.Customer.Domain.Entities;

public class Support : Entity<Guid>
{
    public Support()
    {
        
    }

    public Support(Customer customer,
        string description,
        SupportStatusEnum statu)
    {
        this.Id = Guid.NewGuid();
        this.Customer = customer;
        this.Description = description;
        this.Statu = statu;
    }

    public Customer Customer { get; private set; }
    public string Description { get; private set; }
    public SupportStatusEnum Statu { get; private set; }

    public void Close() => this.Statu = SupportStatusEnum.Close;
    public void Solved() => this.Statu = SupportStatusEnum.Solved;

    public void Update(string description,
        SupportStatusEnum statu)
    {
        this.Description = description;
        this.Statu = statu;
    }

    public static Support Create(Customer customer,
        string description,
        double value,
        DateTime expectedCloseDate)
    {
        return new Support(customer, description, SupportStatusEnum.Open);
    }

}
