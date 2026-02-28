using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Events;

public class InvoiceApprovedEvent : IDomainEvent
{
    public Guid InvoiceId { get; }
    public string InvoiceNumber { get; }
    public int InvoiceType { get; }
    public Guid CustomerId { get; }

    public InvoiceApprovedEvent(Guid invoiceId, string invoiceNumber, int invoiceType, Guid customerId)
    {
        InvoiceId = invoiceId;
        InvoiceNumber = invoiceNumber;
        InvoiceType = invoiceType;
        CustomerId = customerId;
    }
}
