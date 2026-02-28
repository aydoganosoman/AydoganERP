using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Events;

public class InvoiceCreatedEvent : IDomainEvent
{
    public Guid InvoiceId { get; }
    public string InvoiceNumber { get; }
    public int InvoiceType { get; }

    public InvoiceCreatedEvent(Guid invoiceId, string invoiceNumber, int invoiceType)
    {
        InvoiceId = invoiceId;
        InvoiceNumber = invoiceNumber;
        InvoiceType = invoiceType;
    }
}
