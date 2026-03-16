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
    public int EInvoiceScenario { get; private set; } // EInvoiceScenarioEnum: Temel, Ticari, İhracat, Kamu
    public string? PostboxAlias { get; private set; } // GİB Posta Kutusu

    // Additional fields
    public TimeSpan? InvoiceTime { get; private set; } // Fatura saati
    public string? SeriesPrefix { get; private set; } // Seri prefix (ABC)
    public int? InvoiceSerial { get; private set; } // Sıra numarası
    public bool ReplacesInvoiceRef { get; private set; } // İrsaliye yerine geçer

    // Financial adjustments
    public decimal RoundingAmount { get; private set; } // Yuvarlama tutarı
    public decimal PayableAmount { get; private set; } // Ödenecek tutar
    public decimal InvoiceSubDiscount { get; private set; } // Fatura altı iskonto

    // Collections
    public List<InvoiceLine> Lines { get; private set; } = new();
    public List<InvoicePayment> Payments { get; private set; } = new();
    public List<InvoiceNote> InvoiceNotes { get; private set; } = new();
    public List<InvoicePaymentTerm> PaymentTerms { get; private set; } = new();
    public List<InvoiceOrderInfo> OrderInfos { get; private set; } = new();
    public List<InvoicePartyNumber> PartyNumbers { get; private set; } = new();
    public InvoiceOkcInfo? OkcInfo { get; private set; }

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
        bool isEInvoice = false,
        int eInvoiceScenario = 0,
        string? postboxAlias = null,
        TimeSpan? invoiceTime = null,
        string? seriesPrefix = null,
        int? invoiceSerial = null,
        bool replacesInvoiceRef = false)
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
            IsEInvoice = isEInvoice,
            EInvoiceScenario = eInvoiceScenario,
            PostboxAlias = postboxAlias,
            InvoiceTime = invoiceTime,
            SeriesPrefix = seriesPrefix,
            InvoiceSerial = invoiceSerial,
            ReplacesInvoiceRef = replacesInvoiceRef,
            RoundingAmount = 0,
            PayableAmount = 0,
            InvoiceSubDiscount = 0
        };

        invoice.PublishEvent(new InvoiceCreatedEvent(invoice.Id, invoice.InvoiceNumber, invoice.InvoiceType));

        return invoice;
    }

    /// <summary>
    /// Gelen e-fatura için Invoice oluşturur
    /// </summary>
    public static Invoice CreateFromIncoming(
        Guid id,
        Guid companyId,
        Guid supplierId,
        string invoiceNumber,
        DateTime invoiceDate,
        string eInvoiceUUID,
        decimal subTotal,
        decimal vatTotal,
        decimal grandTotal,
        int currency = 0,
        decimal exchangeRate = 1,
        bool isCommercialInvoice = false)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (string.IsNullOrWhiteSpace(invoiceNumber)) throw new ArgumentException("InvoiceNumber is required.");
        if (supplierId == Guid.Empty) throw new ArgumentException("SupplierId cannot be empty.");
        if (string.IsNullOrWhiteSpace(eInvoiceUUID)) throw new ArgumentException("EInvoiceUUID is required.");

        var invoice = new Invoice
        {
            Id = id,
            CompanyId = companyId,
            InvoiceNumber = invoiceNumber.Trim(),
            InvoiceDate = invoiceDate,
            InvoiceType = InvoiceTypeEnum.PurchaseInvoice,
            Status = isCommercialInvoice ? InvoiceStatusEnum.PendingApproval : InvoiceStatusEnum.Received,
            CustomerId = supplierId, // Gelen faturada Customer = Tedarikçi
            SubTotal = subTotal,
            VatTotal = vatTotal,
            DiscountTotal = 0,
            GrandTotal = grandTotal,
            Currency = currency,
            ExchangeRate = exchangeRate,
            PaymentTermDays = 0,
            DueDate = null,
            IsEInvoice = true,
            EInvoiceUUID = eInvoiceUUID
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
        DiscountTotal = Lines.Sum(l => l.DiscountAmount) + InvoiceSubDiscount;
        VatTotal = Lines.Sum(l => l.VatAmount);
        GrandTotal = Lines.Sum(l => l.LineTotalWithVat) - InvoiceSubDiscount + RoundingAmount;
        PayableAmount = GrandTotal;
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

    /// <summary>
    /// Gelen ticari faturayı kabul et
    /// </summary>
    public void AcceptIncomingInvoice()
    {
        if (InvoiceType != InvoiceTypeEnum.PurchaseInvoice && InvoiceType != InvoiceTypeEnum.PurchaseReturn)
            throw new InvalidOperationException("Only incoming invoices can be accepted.");

        if (Status != InvoiceStatusEnum.PendingApproval && Status != InvoiceStatusEnum.Received)
            throw new InvalidOperationException("Invoice is not in a state that can be accepted.");

        Status = InvoiceStatusEnum.AcceptedByUs;
    }

    /// <summary>
    /// Gelen ticari faturayı reddet
    /// </summary>
    public void RejectIncomingInvoice(string reason)
    {
        if (InvoiceType != InvoiceTypeEnum.PurchaseInvoice && InvoiceType != InvoiceTypeEnum.PurchaseReturn)
            throw new InvalidOperationException("Only incoming invoices can be rejected.");

        if (Status != InvoiceStatusEnum.PendingApproval)
            throw new InvalidOperationException("Only invoices pending approval can be rejected.");

        Status = InvoiceStatusEnum.RejectedByUs;
        Notes = $"{Notes}\nRed nedeni: {reason}".Trim();
    }

    /// <summary>
    /// Gelen fatura için: Tedarikçi mü yoksa Müşteri mi?
    /// </summary>
    public bool IsIncomingInvoice => InvoiceType == InvoiceTypeEnum.PurchaseInvoice ||
                                      InvoiceType == InvoiceTypeEnum.PurchaseReturn;

    public void UpdateDetails(
        DateTime invoiceDate,
        int paymentTermDays,
        int currency,
        decimal exchangeRate,
        string? description,
        int eInvoiceScenario = 0,
        string? postboxAlias = null,
        TimeSpan? invoiceTime = null,
        string? seriesPrefix = null,
        int? invoiceSerial = null,
        bool replacesInvoiceRef = false,
        decimal roundingAmount = 0,
        decimal invoiceSubDiscount = 0)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");

        InvoiceDate = invoiceDate;
        PaymentTermDays = paymentTermDays;
        DueDate = paymentTermDays > 0 ? invoiceDate.AddDays(paymentTermDays) : null;
        Currency = currency;
        ExchangeRate = exchangeRate;
        Description = description;
        EInvoiceScenario = eInvoiceScenario;
        PostboxAlias = postboxAlias;
        InvoiceTime = invoiceTime;
        SeriesPrefix = seriesPrefix;
        InvoiceSerial = invoiceSerial;
        ReplacesInvoiceRef = replacesInvoiceRef;
        RoundingAmount = roundingAmount;
        InvoiceSubDiscount = invoiceSubDiscount;
        RecalculateTotals();
    }

    // Note management
    public void AddNote(InvoiceNote note)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");
        InvoiceNotes.Add(note);
    }

    public void RemoveNote(Guid noteId)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");
        var note = InvoiceNotes.FirstOrDefault(n => n.Id == noteId);
        if (note != null) InvoiceNotes.Remove(note);
    }

    // Payment term management
    public void AddPaymentTerm(InvoicePaymentTerm term)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");
        PaymentTerms.Add(term);
    }

    public void RemovePaymentTerm(Guid termId)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");
        var term = PaymentTerms.FirstOrDefault(t => t.Id == termId);
        if (term != null) PaymentTerms.Remove(term);
    }

    // Order info management
    public void AddOrderInfo(InvoiceOrderInfo orderInfo)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");
        OrderInfos.Add(orderInfo);
    }

    public void RemoveOrderInfo(Guid orderInfoId)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");
        var info = OrderInfos.FirstOrDefault(o => o.Id == orderInfoId);
        if (info != null) OrderInfos.Remove(info);
    }

    // Party number management
    public void AddPartyNumber(InvoicePartyNumber partyNumber)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");
        PartyNumbers.Add(partyNumber);
    }

    public void RemovePartyNumber(Guid partyNumberId)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");
        var number = PartyNumbers.FirstOrDefault(p => p.Id == partyNumberId);
        if (number != null) PartyNumbers.Remove(number);
    }

    // OKC info management
    public void SetOkcInfo(InvoiceOkcInfo? okcInfo)
    {
        if (Status != InvoiceStatusEnum.Draft)
            throw new InvalidOperationException("Cannot modify approved or cancelled invoice.");
        OkcInfo = okcInfo;
    }

    public decimal GetPaidAmount() => Payments.Sum(p => p.Amount);

    public decimal GetRemainingAmount() => GrandTotal - GetPaidAmount();

    public bool IsPaid() => GetRemainingAmount() <= 0;
}
