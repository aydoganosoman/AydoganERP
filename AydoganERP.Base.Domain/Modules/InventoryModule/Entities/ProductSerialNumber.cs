using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.InventoryModule.Enums;

namespace AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

public class ProductSerialNumber : AuditableEntity
{
    // For EF
    public ProductSerialNumber() { }

    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
    public string SerialNumber { get; private set; } = default!;
    public SerialNumberStatusEnum Status { get; private set; }
    
    // Alış bilgileri
    public DateTime? PurchaseDate { get; private set; }
    public decimal? PurchasePrice { get; private set; }
    public Guid? PurchaseCustomerId { get; private set; }
    public Customer? PurchaseCustomer { get; private set; }
    
    // Satış bilgileri
    public DateTime? SaleDate { get; private set; }
    public decimal? SalePrice { get; private set; }
    public Guid? SaleCustomerId { get; private set; }
    public Customer? SaleCustomer { get; private set; }
    
    // Garanti bilgisi
    public DateTime? WarrantyEndDate { get; private set; }
    
    public string? Notes { get; private set; }

    public static ProductSerialNumber Create(
        Guid id,
        Guid productId,
        string serialNumber,
        SerialNumberStatusEnum status = SerialNumberStatusEnum.InStock,
        DateTime? purchaseDate = null,
        decimal? purchasePrice = null,
        Guid? purchaseCustomerId = null,
        DateTime? warrantyEndDate = null,
        string? notes = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (productId == Guid.Empty) throw new ArgumentException("ProductId cannot be empty.");
        if (string.IsNullOrWhiteSpace(serialNumber)) throw new ArgumentException("SerialNumber is required.");

        return new ProductSerialNumber
        {
            Id = id,
            ProductId = productId,
            SerialNumber = serialNumber.Trim(),
            Status = status,
            PurchaseDate = purchaseDate,
            PurchasePrice = purchasePrice,
            PurchaseCustomerId = purchaseCustomerId,
            WarrantyEndDate = warrantyEndDate,
            Notes = notes?.Trim()
        };
    }

    public void UpdateStatus(SerialNumberStatusEnum status) => Status = status;

    public void SetPurchaseInfo(DateTime purchaseDate, decimal purchasePrice, Guid customerId)
    {
        PurchaseDate = purchaseDate;
        PurchasePrice = purchasePrice;
        PurchaseCustomerId = customerId;
    }

    public void SetSaleInfo(DateTime saleDate, decimal salePrice, Guid customerId)
    {
        SaleDate = saleDate;
        SalePrice = salePrice;
        SaleCustomerId = customerId;
        Status = SerialNumberStatusEnum.Sold;
    }

    public void ClearSaleInfo()
    {
        SaleDate = null;
        SalePrice = null;
        SaleCustomerId = null;
        Status = SerialNumberStatusEnum.InStock;
    }

    public void SetWarrantyEndDate(DateTime? warrantyEndDate) => WarrantyEndDate = warrantyEndDate;

    public void SetNotes(string? notes) => Notes = notes?.Trim();
}
