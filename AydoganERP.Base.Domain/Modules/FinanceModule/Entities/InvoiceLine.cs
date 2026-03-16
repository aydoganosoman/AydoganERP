using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Entities;

public class InvoiceLine : Entity
{
    // For EF
    public InvoiceLine() { }

    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;
    public int LineNumber { get; private set; }

    // Product
    public Guid? ProductId { get; private set; }
    public Product? Product { get; private set; }
    public string ProductCode { get; private set; } = default!;
    public string ProductName { get; private set; } = default!;
    public string? UnitName { get; private set; }

    // Line type and VAT status
    public int LineType { get; private set; } // InvoiceLineTypeEnum: Product (0) / Service (1)
    public int VatStatus { get; private set; } // VatStatusEnum: Excluded (0) / Included (1)
    public string? GtipCode { get; private set; } // GTİP Kodu (İhracat faturaları için)

    // Amounts
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public float VatRate { get; private set; }
    public float DiscountRate { get; private set; }

    // Calculated
    public decimal LineTotal { get; private set; }        // Quantity * UnitPrice
    public decimal DiscountAmount { get; private set; }   // LineTotal * DiscountRate / 100
    public decimal VatAmount { get; private set; }        // (LineTotal - DiscountAmount) * VatRate / 100
    public decimal LineTotalWithVat { get; private set; } // LineTotal - DiscountAmount + VatAmount

    public string? Description { get; private set; }

    // Serial Number (for serial tracked products)
    public Guid? SerialNumberId { get; private set; }
    public ProductSerialNumber? SerialNumber { get; private set; }

    public static InvoiceLine Create(
        Guid id,
        Guid invoiceId,
        int lineNumber,
        Guid? productId,
        string productCode,
        string productName,
        string? unitName,
        decimal quantity,
        decimal unitPrice,
        float vatRate,
        float discountRate = 0,
        string? description = null,
        Guid? serialNumberId = null,
        int lineType = InvoiceLineTypeEnum.Product,
        int vatStatus = VatStatusEnum.Excluded,
        string? gtipCode = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId cannot be empty.");
        if (string.IsNullOrWhiteSpace(productCode)) throw new ArgumentException("ProductCode is required.");
        if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException("ProductName is required.");
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0.");
        if (vatRate < 0 || vatRate > 100) throw new ArgumentException("VatRate must be between 0 and 100.");
        if (discountRate < 0 || discountRate > 100) throw new ArgumentException("DiscountRate must be between 0 and 100.");

        var line = new InvoiceLine
        {
            Id = id,
            InvoiceId = invoiceId,
            LineNumber = lineNumber,
            ProductId = productId,
            ProductCode = productCode.Trim(),
            ProductName = productName.Trim(),
            UnitName = unitName,
            Quantity = quantity,
            UnitPrice = unitPrice,
            VatRate = vatRate,
            DiscountRate = discountRate,
            Description = description,
            SerialNumberId = serialNumberId,
            LineType = lineType,
            VatStatus = vatStatus,
            GtipCode = gtipCode
        };

        line.Calculate();

        return line;
    }

    public void Update(
        decimal quantity,
        decimal unitPrice,
        float vatRate,
        float discountRate,
        string? description,
        int lineType = InvoiceLineTypeEnum.Product,
        int vatStatus = VatStatusEnum.Excluded,
        string? gtipCode = null)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0.");
        if (vatRate < 0 || vatRate > 100) throw new ArgumentException("VatRate must be between 0 and 100.");
        if (discountRate < 0 || discountRate > 100) throw new ArgumentException("DiscountRate must be between 0 and 100.");

        Quantity = quantity;
        UnitPrice = unitPrice;
        VatRate = vatRate;
        DiscountRate = discountRate;
        Description = description;
        LineType = lineType;
        VatStatus = vatStatus;
        GtipCode = gtipCode;

        Calculate();
    }

    public void SetSerialNumber(Guid? serialNumberId)
    {
        SerialNumberId = serialNumberId;
    }

    private void Calculate()
    {
        if (VatStatus == VatStatusEnum.Included)
        {
            // KDV Dahil: Birim fiyat KDV dahildir, KDV'yi ters hesapla
            var grossTotal = Quantity * UnitPrice;
            DiscountAmount = grossTotal * (decimal)DiscountRate / 100m;
            var afterDiscount = grossTotal - DiscountAmount;
            VatAmount = afterDiscount - (afterDiscount / (1 + (decimal)VatRate / 100m));
            LineTotal = afterDiscount - VatAmount;
            LineTotalWithVat = afterDiscount;
        }
        else
        {
            // KDV Hariç: Normal hesaplama
            LineTotal = Quantity * UnitPrice;
            DiscountAmount = LineTotal * (decimal)DiscountRate / 100m;
            var afterDiscount = LineTotal - DiscountAmount;
            VatAmount = afterDiscount * (decimal)VatRate / 100m;
            LineTotalWithVat = afterDiscount + VatAmount;
        }
    }
}
