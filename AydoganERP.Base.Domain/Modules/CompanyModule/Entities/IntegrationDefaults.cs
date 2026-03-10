using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CompanyModule.Entities;

public class IntegrationDefaults : Entity
{
    public Guid Id { get; private set; }
    public Guid IntegrationId { get; private set; }

    // Sipariş Ayarları
    public bool ConsiderOrderStatuses { get; private set; }
    public string? OrderStatuses { get; private set; }
    public bool AutoCreateBarcode { get; private set; }

    // Fatura Bilgileri
    public int? InvoiceDateType { get; private set; }
    public decimal DefaultVatRate { get; private set; }
    public string? VatExemptionCode { get; private set; }
    public string? ExportVatExemptionCode { get; private set; }
    public Guid? ShippingFeeAccountId { get; private set; }
    public Guid? InstallmentFeeAccountId { get; private set; }
    public Guid? DefaultCustomerId { get; private set; }
    public int? PaymentMethod { get; private set; }
    public Guid? CargoCompanyId { get; private set; }
    public Guid? DefaultCategoryId { get; private set; }
    public Guid? EInvoiceSeriesId { get; private set; }
    public Guid? EArchiveSeriesId { get; private set; }

    // Sipariş Aktarım Ayarları
    public int OrderFilterDaysBefore { get; private set; }

    // Aktarım Durum Özeti (readonly)
    public DateTime? LastSyncTime { get; private set; }
    public DateTime? LastOrderFilterDate { get; private set; }
    public string? LastSyncStatus { get; private set; }

    // Navigation
    public ECommerceIntegration Integration { get; private set; } = null!;

    private IntegrationDefaults() { }

    public static IntegrationDefaults Create(Guid id, Guid integrationId)
    {
        return new IntegrationDefaults
        {
            Id = id,
            IntegrationId = integrationId,
            ConsiderOrderStatuses = false,
            AutoCreateBarcode = false,
            DefaultVatRate = 0,
            OrderFilterDaysBefore = 1
        };
    }

    public void Update(
        bool considerOrderStatuses,
        string? orderStatuses,
        bool autoCreateBarcode,
        int? invoiceDateType,
        decimal defaultVatRate,
        string? vatExemptionCode,
        string? exportVatExemptionCode,
        Guid? shippingFeeAccountId,
        Guid? installmentFeeAccountId,
        Guid? defaultCustomerId,
        int? paymentMethod,
        Guid? cargoCompanyId,
        Guid? defaultCategoryId,
        Guid? eInvoiceSeriesId,
        Guid? eArchiveSeriesId,
        int orderFilterDaysBefore)
    {
        ConsiderOrderStatuses = considerOrderStatuses;
        OrderStatuses = orderStatuses;
        AutoCreateBarcode = autoCreateBarcode;
        InvoiceDateType = invoiceDateType;
        DefaultVatRate = defaultVatRate;
        VatExemptionCode = vatExemptionCode;
        ExportVatExemptionCode = exportVatExemptionCode;
        ShippingFeeAccountId = shippingFeeAccountId;
        InstallmentFeeAccountId = installmentFeeAccountId;
        DefaultCustomerId = defaultCustomerId;
        PaymentMethod = paymentMethod;
        CargoCompanyId = cargoCompanyId;
        DefaultCategoryId = defaultCategoryId;
        EInvoiceSeriesId = eInvoiceSeriesId;
        EArchiveSeriesId = eArchiveSeriesId;
        OrderFilterDaysBefore = orderFilterDaysBefore;
    }

    public void UpdateSyncStatus(DateTime syncTime, DateTime? orderFilterDate, string status)
    {
        LastSyncTime = syncTime;
        LastOrderFilterDate = orderFilterDate;
        LastSyncStatus = status;
    }
}
