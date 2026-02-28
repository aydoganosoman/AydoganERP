using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Entities;

public class InvoicePayment : Entity
{
    // For EF
    public InvoicePayment() { }

    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;

    public DateTime PaymentDate { get; private set; }
    public decimal Amount { get; private set; }
    public int PaymentMethod { get; private set; }
    public string? Reference { get; private set; }
    public string? Notes { get; private set; }

    public static InvoicePayment Create(
        Guid id,
        Guid invoiceId,
        DateTime paymentDate,
        decimal amount,
        int paymentMethod = PaymentMethodEnum.Cash,
        string? reference = null,
        string? notes = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId cannot be empty.");
        if (amount <= 0) throw new ArgumentException("Amount must be greater than 0.");

        return new InvoicePayment
        {
            Id = id,
            InvoiceId = invoiceId,
            PaymentDate = paymentDate,
            Amount = amount,
            PaymentMethod = paymentMethod,
            Reference = reference,
            Notes = notes
        };
    }

    public void Update(DateTime paymentDate, decimal amount, int paymentMethod, string? reference, string? notes)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than 0.");

        PaymentDate = paymentDate;
        Amount = amount;
        PaymentMethod = paymentMethod;
        Reference = reference;
        Notes = notes;
    }
}
