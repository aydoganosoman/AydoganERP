using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Customer.Domain.Entities;

public class QuoteDetail : Entity<Guid>
{
    public QuoteDetail()
    {
        
    }

    public QuoteDetail(Quote quote,
        float quantity,
        double unitPrice,
        double discount)
    {
        this.Id = Guid.NewGuid();
        this.Quote = quote;
        this.Quantity = quantity;
        this.UnitPrice = unitPrice;
        this.Discount = discount;
    }

    public Quote Quote { get; private set; }
    public float Quantity { get; private set; }
    public double UnitPrice { get; private set; }
    public double Discount { get; private set; }

    public void Update(float quantity,
        double unitPrice,
        double discount)
    {
        this.Quantity = quantity;
        this.UnitPrice = unitPrice;
        this.Discount = discount;
    }

    public static QuoteDetail Create(Quote quote,
        float quantity,
        double unitPrice,
        double discount)
    {
        return new QuoteDetail(quote, quantity, unitPrice, discount);
    }
}
