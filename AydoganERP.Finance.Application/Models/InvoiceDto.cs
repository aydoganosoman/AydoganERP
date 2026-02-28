namespace AydoganERP.Finance.Application.Models;

public class InvoiceDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string InvoiceNumber { get; set; } = default!;
    public DateTime InvoiceDate { get; set; }
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

    public bool IsEInvoice { get; set; }
    public string? EInvoiceUUID { get; set; }

    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public bool IsPaid { get; set; }

    public List<InvoiceLineDto> Lines { get; set; } = new();
    public List<InvoicePaymentDto> Payments { get; set; } = new();

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
}
