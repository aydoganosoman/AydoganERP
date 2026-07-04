namespace AydoganERP.Finance.Application.Models;

public class InvoiceDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string InvoiceNumber { get; set; } = default!;
    public DateTime InvoiceDate { get; set; }
    public TimeSpan? InvoiceTime { get; set; }
    public int InvoiceType { get; set; }
    public string InvoiceTypeName { get; set; } = default!;
    public int Status { get; set; }
    public string StatusName { get; set; } = default!;

    public Guid CustomerId { get; set; }
    public string CustomerCode { get; set; } = default!;
    public string CustomerName { get; set; } = default!;

    public decimal SubTotal { get; set; }
    public decimal VatTotal { get; set; }
    public decimal DiscountTotal { get; set; }
    public decimal GrandTotal { get; set; }
    public int Currency { get; set; }
    public decimal ExchangeRate { get; set; }

    public DateTime? DueDate { get; set; }
    public int PaymentTermDays { get; set; }

    public string? Description { get; set; }
    public string? Notes { get; set; }

    // E-Invoice
    public bool IsEInvoice { get; set; }
    public string? EInvoiceUUID { get; set; }
    public int EInvoiceScenario { get; set; }
    public string? PostboxAlias { get; set; }

    // Additional fields
    public string? SeriesPrefix { get; set; }
    public int? InvoiceSerial { get; set; }
    public bool ReplacesInvoiceRef { get; set; }

    // Financial adjustments
    public decimal RoundingAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public decimal InvoiceSubDiscount { get; set; }

    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public bool IsPaid { get; set; }

    // Collections
    public List<InvoiceLineDto> Lines { get; set; } = new();
    public List<InvoicePaymentDto> Payments { get; set; } = new();
    public List<InvoiceNoteDto> InvoiceNotes { get; set; } = new();
    public List<InvoicePaymentTermDto> PaymentTerms { get; set; } = new();
    public List<InvoiceOrderInfoDto> OrderInfos { get; set; } = new();
    public List<InvoicePartyNumberDto> PartyNumbers { get; set; } = new();
    public InvoiceOkcInfoDto? OkcInfo { get; set; }

    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
}

public class InvoiceLineDto
{
    public Guid Id { get; set; }
    public int LineNumber { get; set; }
    public Guid? ProductId { get; set; }
    public string ProductCode { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public string? UnitName { get; set; }

    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public float VatRate { get; set; }
    public float DiscountRate { get; set; }

    // New fields
    public int LineType { get; set; } // 0=Product, 1=Service
    public int VatStatus { get; set; } // 0=Excluded, 1=Included
    public string? GtipCode { get; set; }

    public decimal LineTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal LineTotalWithVat { get; set; }

    public string? Description { get; set; }
    public Guid? SerialNumberId { get; set; }
    public string? SerialNumber { get; set; }
}

public class InvoicePaymentDto
{
    public Guid Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public int PaymentMethod { get; set; }
    public string PaymentMethodName { get; set; } = default!;
    public string? Reference { get; set; }
    public string? Notes { get; set; }
}

public class InvoiceListDto
{
    public Guid Id { get; set; }
    public string InvoiceNumber { get; set; } = default!;
    public DateTime InvoiceDate { get; set; }
    public int InvoiceType { get; set; }
    public string InvoiceTypeName { get; set; } = default!;
    public int Status { get; set; }
    public string StatusName { get; set; } = default!;
    public string CustomerCode { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
    public decimal GrandTotal { get; set; }
    public int Currency { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public bool IsPaid { get; set; }
    public bool IsEInvoice { get; set; }
    public string? EInvoiceUUID { get; set; }
    public int EInvoiceScenario { get; set; }
}

// Yeni DTO'lar
public class InvoiceNoteDto
{
    public Guid Id { get; set; }
    public string NoteText { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class InvoicePaymentTermDto
{
    public Guid Id { get; set; }
    public int PaymentMethod { get; set; }
    public string PaymentMethodName { get; set; } = default!;
    public DateTime? DueDate { get; set; }
    public decimal Amount { get; set; }
    public decimal? PenaltyRate { get; set; }
    public decimal? PenaltyAmount { get; set; }
    public string? Description { get; set; }
}

public class InvoiceOrderInfoDto
{
    public Guid Id { get; set; }
    public string? OrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? WaybillNumber { get; set; }
    public DateTime? WaybillDate { get; set; }
    public string? DocumentPath { get; set; }
    public string? DocumentName { get; set; }
}

public class InvoicePartyNumberDto
{
    public Guid Id { get; set; }
    public bool IsBuyer { get; set; }
    public int NumberType { get; set; }
    public string NumberTypeName { get; set; } = default!;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class InvoiceOkcInfoDto
{
    public Guid Id { get; set; }
    public string? FisNo { get; set; }
    public DateTime? FisDate { get; set; }
    public TimeSpan? FisTime { get; set; }
    public int? FisType { get; set; }
    public string? FisTypeName { get; set; }
    public string? ZReportNo { get; set; }
    public string? OkcSerialNo { get; set; }
}

public class DocumentDto
{
    public Guid Id { get; set; }
    public int AttachmentType { get; set; }
    public Guid RelatedEntityId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string? ContentType { get; set; }
    public long FileSize { get; set; }
    public string? Description { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
}
