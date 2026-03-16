namespace AydoganERP.EInvoice.MySoft.Models.Request;

/// <summary>
/// MySoft fatura gönderim isteği
/// </summary>
public class InvoiceOutboxRequest
{
    public int Id { get; set; } = -1;
    public string? ConnectorGuid { get; set; }
    public int EDocumentType { get; set; }
    public int Profile { get; set; }
    public int InvoiceType { get; set; }
    public string? Ettn { get; set; }
    public string? Prefix { get; set; }
    public string? DocNo { get; set; }
    public string? DocDate { get; set; }
    public string? DocTime { get; set; }
    public string? CurrencyCode { get; set; }
    public double CurrencyRate { get; set; }
    public string? SenderType { get; set; }
    public string? PkAlias { get; set; }
    public string? GbAlias { get; set; }
    public string? TenantIdentifierNumber { get; set; }
    public string? ReferanceKey { get; set; }
    public bool IsCalculateByApi { get; set; }
    public bool IsManuelCalculation { get; set; }
    public bool isAddPayableAmountString { get; set; }

    // İade fatura bilgileri
    public string? BillingRefInvoiceNo { get; set; }
    public string? BillingRefInvoiceDate { get; set; }
    public string? BillingRefNote { get; set; }

    // Sipariş/İrsaliye
    public string? OrderNo { get; set; }
    public string? OrderDate { get; set; }
    public List<WaybillInfoModel>? WaybillInfo { get; set; }

    // Alıcı bilgileri
    public InvoiceAccountModel InvoiceAccount { get; set; } = new();

    // Tutar hesaplamaları
    public InvoiceCalculationModel? InvoiceCalculation { get; set; }

    // Satırlar
    public List<InvoiceDetailModel> InvoiceDetail { get; set; } = new();

    // Notlar
    public List<NoteModel> Notes { get; set; } = new();

    // Kamu faturası için ödeme bilgileri
    public List<PaymentMeansModel>? PaymentMeans { get; set; }
    public string? PublicServicePayeeVKN { get; set; }
    public string? PublicServicePayeePartyName { get; set; }
    public string? PublicServicePayeeCountry { get; set; }
    public string? PublicServicePayeeCity { get; set; }
    public string? PublicServicePayeeCitysubdivision { get; set; }
}

public class InvoiceAccountModel
{
    public string? VknTckn { get; set; }
    public string? AccountName { get; set; }
    public string? TaxOfficeName { get; set; }
    public string? CountryName { get; set; }
    public string? CityName { get; set; }
    public string? CitySubdivision { get; set; }
    public string? StreetName { get; set; }
    public string? Email1 { get; set; }
}

public class InvoiceCalculationModel
{
    public double LineExtensionAmount { get; set; }
    public double TaxExclusiveAmount { get; set; }
    public double TaxInclusiveAmount { get; set; }
    public double PayableAmount { get; set; }
    public double AllowanceTotalAmount { get; set; }
}

public class InvoiceDetailModel
{
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? UnitCode { get; set; }
    public double Qty { get; set; }
    public double UnitPriceTra { get; set; }
    public double AmtTra { get; set; }
    public double VatRate { get; set; }
    public double AmtVatTra { get; set; }
    public double TaxableAmtTra { get; set; }
    public string? Note { get; set; }
    public string? TaxExemptionReasonCode { get; set; }
    public string? TaxExemptionReasonName { get; set; }
    public string? WithholdingTaxTypeCode { get; set; }
    public string? WithholdingTaxTypeName { get; set; }
    public int WithholdingTaxPercentage { get; set; }
    public decimal WithholdingTaxableAmount { get; set; }
    public decimal WithholdingTaxAmount { get; set; }
    public bool IsSubtractDiscountFromAmtTra { get; set; }
    public List<AllowanceChargeModel> AllowanceCharge { get; set; } = new();
    public List<ItemInstanceModel> ItemInstance { get; set; } = new();
}

public class AllowanceChargeModel
{
    public bool ChargeIndicator { get; set; }
    public double MultiplierFactorNumeric { get; set; }
    public int SequenceNumeric { get; set; }
    public double Amount { get; set; }
    public double BaseAmount { get; set; }
}

public class ItemInstanceModel
{
    public string? SerialId { get; set; }
}

public class NoteModel
{
    public string? Note { get; set; }
}

public class WaybillInfoModel
{
    public string? WaybillNo { get; set; }
    public string? WaybillDate { get; set; }
}

public class PaymentMeansModel
{
    public string? PaymentMeansCode { get; set; }
    public string? PaymentChannelCode { get; set; }
    public FinancialAccountModel? PayeeFinancialAccount { get; set; }
}

public class FinancialAccountModel
{
    public string? CurrencyCode { get; set; }
    public string? ID { get; set; }
    public string? PaymentNote { get; set; }
}
