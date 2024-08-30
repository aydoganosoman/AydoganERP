using AydoganERP.Base.Domain.Common;
using AydoganERP.Customer.Domain.Enums;

namespace AydoganERP.Customer.Domain.Entities;

public class Order : Entity<Guid>
{
    public Order()
    {
        
    }

    private Order(Customer customer,
        DateTime date,
        DateTime deliveryDate,
        OrderStatu statu)
    {
        this.Id = Guid.NewGuid();
        this.Customer = customer;
        this.DeliveryDate = deliveryDate;
        this.Statu = statu;
    }

    public Customer Customer { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime DeliveryDate { get; private set; }
    public OrderStatu Statu { get; private set; }

    public virtual List<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();

    public void InProcess() => this.Statu = OrderStatu.InProcess;
    public void Completed() => this.Statu = OrderStatu.Completed;
    public void Cancelled() => this.Statu = OrderStatu.Cancelled;

    public void Update(DateTime date,
        DateTime deliveryDate,
        OrderStatu statu)
    {
        this.Date = date;
        this.DeliveryDate = deliveryDate;
        this.Statu = statu;
    }

    public static Order Create(Customer customer,
        DateTime date,
        DateTime deliveryDate)
    {
        return new Order(customer, date, deliveryDate, OrderStatu.New);

    }

    public void AddDetail(OrderDetail orderDetail)
    {
        this.OrderDetails.Add(orderDetail);
    }

}
