namespace AydoganERP.Company.Application.Models;

public class IntegrationDefaultsDto
{
    public Guid Id { get; set; }
    public Guid IntegrationId { get; set; }

    // Sipariş Ayarları
    public bool ConsiderOrderStatuses { get; set; }
    public string? OrderStatuses { get; set; }
    public bool AutoCreateBarcode { get; set; }

    // Fatura Bilgileri
    public int? InvoiceDateType { get; set; }
    public decimal DefaultVatRate { get; set; }
    public string? VatExemptionCode { get; set; }
    public string? ExportVatExemptionCode { get; set; }
    public Guid? ShippingFeeAccountId { get; set; }
    public Guid? InstallmentFeeAccountId { get; set; }
    public Guid? DefaultCustomerId { get; set; }
    public int? PaymentMethod { get; set; }
    public Guid? CargoCompanyId { get; set; }
    public Guid? DefaultCategoryId { get; set; }
    public Guid? EInvoiceSeriesId { get; set; }
    public Guid? EArchiveSeriesId { get; set; }

    // Sipariş Aktarım Ayarları
    public int OrderFilterDaysBefore { get; set; }

    // Aktarım Durum Özeti
    public DateTime? LastSyncTime { get; set; }
    public DateTime? LastOrderFilterDate { get; set; }
    public string? LastSyncStatus { get; set; }
}
