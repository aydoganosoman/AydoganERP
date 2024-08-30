using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Customer.Domain.Entities;

public class Quote : Entity<Guid>
{
    public Quote()
    {
        
    }

    public Quote(Customer customer,
        DateTime quotationDate,
        DateTime expirationDate)
    {
        this.Id = Guid.NewGuid();
        this.Customer = customer;
        this.QuotationDate = quotationDate;
        this.ExpirationDate = expirationDate;
    }

    public Customer Customer { get; private set; }
    public DateTime QuotationDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }

    public virtual List<QuoteDetail> QuoteDetails { get; private set; } = new List<QuoteDetail>();

    public void Update(Customer customer,
        DateTime quotationDate,
        DateTime expirationDate)
    {
        this.QuotationDate = quotationDate;
        this.ExpirationDate = expirationDate;
    }

    public static Quote Create(Customer customer,
        DateTime quotationDate,
        DateTime expirationDate)
    {
        return new Quote(customer, quotationDate, expirationDate);
    }

    public void AddDetail(QuoteDetail quoteDetail)
    {
        this.QuoteDetails.Add(quoteDetail);
    }

}
