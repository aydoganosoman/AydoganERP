using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.FinanceModule.Entities;

/// <summary>
/// Sipariş ve irsaliye referans bilgileri
/// </summary>
public class InvoiceOrderInfo : Entity
{
    // For EF
    public InvoiceOrderInfo() { }

    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;
    
    public string? OrderNumber { get; private set; } // Sipariş No
    public DateTime? OrderDate { get; private set; } // Sipariş Tarihi
    public string? WaybillNumber { get; private set; } // İrsaliye No
    public DateTime? WaybillDate { get; private set; } // İrsaliye Tarihi
    public string? DocumentPath { get; private set; } // Ek doküman yolu
    public string? DocumentName { get; private set; } // Doküman adı

    public static InvoiceOrderInfo Create(
        Guid id,
        Guid invoiceId,
        string? orderNumber = null,
        DateTime? orderDate = null,
        string? waybillNumber = null,
        DateTime? waybillDate = null,
        string? documentPath = null,
        string? documentName = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId cannot be empty.");

        return new InvoiceOrderInfo
        {
            Id = id,
            InvoiceId = invoiceId,
            OrderNumber = orderNumber?.Trim(),
            OrderDate = orderDate,
            WaybillNumber = waybillNumber?.Trim(),
            WaybillDate = waybillDate,
            DocumentPath = documentPath,
            DocumentName = documentName
        };
    }

    public void Update(
        string? orderNumber,
        DateTime? orderDate,
        string? waybillNumber,
        DateTime? waybillDate,
        string? documentPath,
        string? documentName)
    {
        OrderNumber = orderNumber?.Trim();
        OrderDate = orderDate;
        WaybillNumber = waybillNumber?.Trim();
        WaybillDate = waybillDate;
        DocumentPath = documentPath;
        DocumentName = documentName;
    }
}
