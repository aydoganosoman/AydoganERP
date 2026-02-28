using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using AydoganERP.Base.Domain.Modules.FinanceModule.Events;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Entities;

public class Invoice : Entity
{
    // For EF
    public Invoice() { }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = null!;
    public string InvoiceNumber { get; private set; } = default!;
    public DateTime InvoiceDate { get; private set; }
    public int InvoiceType { get; private set; }
    public int Status { get; private set; }

    // Customer/Supplier
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;

    // Amounts
    public decimal SubTotal { get; private set; }
    public decimal VatTotal { get; private set; }
    public decimal DiscountTotal { get; private set; }
    public decimal GrandTotal { get; private set; }
    public int Currency { get; private set; } // 0=TRY, 1=USD, 2=EUR
    public decimal ExchangeRate { get; private set; } = 1;

    // Payment terms
    public DateTime? DueDate { get; private set; }
    public int PaymentTermDays { get; private set; }

    // Notes
    public string? Description { get; private set; }
    public string? Notes { get; private set; }

    // E-Invoice
    public string? EInvoiceUUID { get; private set; }
    public bool IsEInvoice { get; private set; }

    // Collections
    public List<InvoiceLine> Lines { get; private set; } = new();
    public List<InvoicePayment> Payments { get; private set; } = new();

    public static Invoice Create(
        Guid id,
        Guid companyId,
        string invoiceNumber,
        DateTime invoiceDate,
        int invoiceType,
        Guid customerId,
        int currency = 0,
        decimal exchangeRate = 1,
        int paymentTermDays = 0,
        string? description = null,
        string? notes = null,
        bool isEInvoice = false)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (string.IsNullOrWhiteSpace(invoiceNumber)) throw new ArgumentException("InvoiceNumber is required.");
        if (customerId == Guid.Empty) throw new ArgumentException("CustomerId cannot be empty.");

        var invoice = new Invoice
        {
            Id = id,
            CompanyId = companyId,
            InvoiceNumber = invoiceNumber.Trim(),
            InvoiceDate = invoiceDate,
            InvoiceType = invoiceType,
            Status = InvoiceStatusEnum.Draft,
            CustomerId = customerId,
            SubTotal = 0,
            VatTotal = 0,
            DiscountTotal = 0,
            GrandTotal = 0,
            Currency = currency,
            ExchangeRate = exchangeRate,
            PaymentTermDays = paymentTermDays,
            DueDate = paymentTermDays > 0 ? invoiceDate.AddDays(paymentTermDays) : null,
            Description = description,
            Notes = notes,
            IsEInvoice = isEInvoice
        };

        invoice.PublishEvent(new InvoiceCreatedEvent(invoice.Id, invoice.InvoiceNumber, invoice.InvoiceType));

        return invoice;
    }

    public void AddLine(InvoiceLine line)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");

        Lines.Add(line);
        RecalculateTotals();
    }

    public void RemoveLine(Guid lineId)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");

        var line = Lines.FirstOrDefault(l => l.Id == lineId);
        if (line != null)
        {
            Lines.Remove(line);
            RecalculateTotals();
        }
    }

    public void UpdateLine(Guid lineId, decimal quantity, decimal unitPrice, float vatRate, float discountRate, string? description)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");

        var line = Lines.FirstOrDefault(l => l.Id == lineId);
        if (line != null)
        {
            line.Update(quantity, unitPrice, vatRate, discountRate, description);
            RecalculateTotals();
        }
    }

    public void RecalculateTotals()
    {
        SubTotal = Lines.Sum(l => l.LineTotal);
        DiscountTotal = Lines.Sum(l => l.DiscountAmount);
        VatTotal = Lines.Sum(l => l.VatAmount);
        GrandTotal = Lines.Sum(l => l.LineTotalWithVat);
    }

    public void Approve()
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Only draft invoices can be approved.");

        if (!Lines.Any())
            throw new InvalidOperationException("Invoice must have at least one line.");

        Status = InvoiceStatusEnum.Approved;
        PublishEvent(new InvoiceApprovedEvent(Id, InvoiceNumber, InvoiceType, CustomerId));
    }

    public void Cancel(string? reason = null)
    {
        if (Status == InvoiceStatusEnum.Cancelled)
            throw new InvalidOperationException("Invoice is already cancelled.");

        if (Status == InvoiceStatusEnum.EInvoiceSent || Status == InvoiceStatusEnum.EInvoiceAccepted)
            throw new InvalidOperationException("Cannot cancel e-invoice. Create a return invoice instead.");

        Status = InvoiceStatusEnum.Cancelled;
        if (!string.IsNullOrWhiteSpace(reason))
            Notes = $"{Notes}\nİptal nedeni: {reason}".Trim();

        PublishEvent(new InvoiceCancelledEvent(Id, InvoiceNumber));
    }

    public void SetEInvoiceUUID(string uuid)
    {
        EInvoiceUUID = uuid;
        Status = InvoiceStatusEnum.EInvoiceSent;
    }

    public void SetEInvoiceAccepted()
    {
        Status = InvoiceStatusEnum.EInvoiceAccepted;
    }

    public void SetEInvoiceRejected()
    {
        Status = InvoiceStatusEnum.EInvoiceRejected;
    }

    public void UpdateDetails(
        DateTime invoiceDate,
        int paymentTermDays,
        int currency,
        decimal exchangeRate,
        string? description,
        string? notes)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");

        InvoiceDate = invoiceDate;
        PaymentTermDays = paymentTermDays;
        DueDate = paymentTermDays > 0 ? invoiceDate.AddDays(paymentTermDays) : null;
        Currency = currency;
        ExchangeRate = exchangeRate;
        Description = description;
        Notes = notes;
    }

    public decimal GetPaidAmount() => Payments.Sum(p => p.Amount);

    public decimal GetRemainingAmount() => GrandTotal - GetPaidAmount();

    public bool IsPaid() => GetRemainingAmount() <= 0;
}
