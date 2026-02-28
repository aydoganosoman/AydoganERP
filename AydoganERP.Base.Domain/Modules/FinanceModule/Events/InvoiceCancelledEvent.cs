using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Events;

public class InvoiceCancelledEvent : IDomainEvent
{
    public Guid InvoiceId { get; }
    public string InvoiceNumber { get; }

    public InvoiceCancelledEvent(Guid invoiceId, string invoiceNumber)
    {
        InvoiceId = invoiceId;
        InvoiceNumber = invoiceNumber;
    }
}
