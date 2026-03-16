using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Entities;

/// <summary>
/// Ödeme koşulları - vade ve gecikme cezası bilgileri
/// </summary>
public class InvoicePaymentTerm : Entity
{
    // For EF
    public InvoicePaymentTerm() { }

    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;
    
    public int PaymentMethod { get; private set; } // PaymentMethodEnum
    public DateTime? DueDate { get; private set; }
    public decimal Amount { get; private set; }
    public decimal? PenaltyRate { get; private set; } // Gecikme ceza oranı %
    public decimal? PenaltyAmount { get; private set; } // Gecikme ceza tutarı
    public string? Description { get; private set; }

    public static InvoicePaymentTerm Create(
        Guid id,
        Guid invoiceId,
        int paymentMethod,
        decimal amount,
        DateTime? dueDate = null,
        decimal? penaltyRate = null,
        decimal? penaltyAmount = null,
        string? description = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId cannot be empty.");
        if (amount < 0) throw new ArgumentException("Amount cannot be negative.");

        return new InvoicePaymentTerm
        {
            Id = id,
            InvoiceId = invoiceId,
            PaymentMethod = paymentMethod,
            Amount = amount,
            DueDate = dueDate,
            PenaltyRate = penaltyRate,
            PenaltyAmount = penaltyAmount,
            Description = description
        };
    }

    public void Update(
        int paymentMethod,
        decimal amount,
        DateTime? dueDate,
        decimal? penaltyRate,
        decimal? penaltyAmount,
        string? description)
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative.");

        PaymentMethod = paymentMethod;
        Amount = amount;
        DueDate = dueDate;
        PenaltyRate = penaltyRate;
        PenaltyAmount = penaltyAmount;
        Description = description;
    }
}
