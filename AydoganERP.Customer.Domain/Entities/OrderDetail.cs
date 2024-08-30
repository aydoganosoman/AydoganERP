using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Customer.Domain.Entities;

public class OrderDetail : Entity<Guid>
{
    public OrderDetail()
    {
        
    }

    public OrderDetail(Order order,
        float quantity,
        double unitPrice,
        double discount,
        decimal totalPrice)
    {
        this.Id = Guid.NewGuid();
        this.Order = order;
        this.Quantity = quantity;
        this.UnitPrice = unitPrice;
        this.Discount = discount;
        this.TotalPrice = totalPrice;
    }

    public Order Order { get; private set; }
    public float Quantity { get; private set; }
    public double UnitPrice { get; private set; }
    public double Discount { get; private set; }
    public decimal TotalPrice { get; private set; }

    public void Update(float quantity,
        double unitPrice,
        double discount,
        decimal totalPrice)
    {
        this.Quantity = quantity;
        this.UnitPrice = unitPrice;
        this.Discount = discount;
        this.TotalPrice = totalPrice;
    }

    public static OrderDetail Create(Order order,
        float quantity,
        double unitPrice,
        double discount,
        decimal totalPrice)
    {
        return new OrderDetail(order, quantity, unitPrice, discount, totalPrice);
    }

}
