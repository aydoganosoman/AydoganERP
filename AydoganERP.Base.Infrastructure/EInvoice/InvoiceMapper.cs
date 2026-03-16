using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using AydoganERP.EInvoice.Abstractions.Enums;
using AydoganERP.EInvoice.Abstractions.Models;

namespace AydoganERP.Base.Infrastructure.EInvoice;

/// <summary>
/// Invoice entity'sini EInvoiceRequest'e dönüştürür
/// </summary>
public static class InvoiceMapper
{
    public static EInvoiceRequest ToEInvoiceRequest(
        Invoice invoice,
        Company company,
        Customer customer,
        EInvoiceDocumentType documentType,
        EInvoiceScenario scenario)
    {
        var request = new EInvoiceRequest
        {
            // Belge bilgileri
            InvoiceId = invoice.Id.ToString(),
            DocumentType = documentType,
            Scenario = scenario,
            InvoiceType = MapInvoiceType(invoice.InvoiceType),
            InvoiceNumber = invoice.InvoiceNumber,
            InvoiceDate = invoice.InvoiceDate,
            InvoiceTime = invoice.InvoiceDate.TimeOfDay,

            // Para birimi
            Currency = (CurrencyType)invoice.Currency,
            ExchangeRate = invoice.ExchangeRate,

            // Gönderici (Firma)
            SenderTaxNumber = company.TaxInfo?.TaxNumber ?? string.Empty,
            SenderTitle = company.Name,
            SenderTaxOffice = company.TaxInfo?.TaxOffice,

            // Alıcı (Müşteri)
            ReceiverTaxNumber = customer.TaxInfo?.TaxNumber ?? string.Empty,
            ReceiverTitle = customer.CustomerName,
            ReceiverTaxOffice = customer.TaxInfo?.TaxOffice,
            ReceiverCity = customer.Address?.City?.ToString(),
            ReceiverDistrict = customer.Address?.District?.ToString(),
            ReceiverAddress = customer.Address?.Line,
            ReceiverEmail = customer.Contact?.Email,
            ReceiverPhone = customer.Contact?.Phone,

            // Tutarlar
            SubTotal = invoice.SubTotal,
            DiscountTotal = invoice.DiscountTotal,
            TaxableAmount = invoice.SubTotal - invoice.DiscountTotal,
            VatTotal = invoice.VatTotal,
            GrandTotal = invoice.GrandTotal,
            PayableAmount = invoice.GrandTotal,

            // Açıklama
            Description = invoice.Description
        };

        // Notlar
        if (!string.IsNullOrEmpty(invoice.Notes))
        {
            request.Notes.Add(invoice.Notes);
        }

        // Satırlar
        var lineNumber = 1;
        foreach (var line in invoice.Lines)
        {
            request.Lines.Add(new EInvoiceRequestLine
            {
                LineNumber = lineNumber++,
                ProductCode = line.ProductCode ?? $"ITEM{lineNumber}",
                ProductName = line.ProductName,
                Quantity = line.Quantity,
                UnitCode = MapUnitCode(line.UnitName),
                UnitPrice = line.UnitPrice,
                LineTotal = line.LineTotal,
                DiscountRate = (decimal)line.DiscountRate,
                DiscountAmount = line.DiscountAmount,
                VatRate = (decimal)line.VatRate,
                VatAmount = line.VatAmount,
                LineTotalWithVat = line.LineTotalWithVat,
                Description = line.Description
            });
        }

        return request;
    }

    private static EInvoiceType MapInvoiceType(int invoiceType)
    {
        // InvoiceTypeEnum değerlerine göre mapping
        return invoiceType switch
        {
            0 => EInvoiceType.Sales,      // Satış
            1 => EInvoiceType.Sales,      // Alış (gelen fatura)
            2 => EInvoiceType.Return,     // İade
            _ => EInvoiceType.Sales
        };
    }

    private static string MapUnitCode(string? unitCode)
    {
        // Birim kodu mapping (GİB standart kodları)
        return unitCode?.ToUpperInvariant() switch
        {
            "ADET" or "AD" or "PCS" => "C62",
            "KG" or "KGM" => "KGM",
            "MT" or "MTR" or "METRE" => "MTR",
            "LT" or "LTR" or "LITRE" => "LTR",
            "M2" or "MTK" => "MTK",
            "M3" or "MTQ" => "MTQ",
            "PAKET" or "PK" => "PA",
            "KUTU" or "BX" => "BX",
            "SAAT" or "HUR" => "HUR",
            "GUN" or "DAY" => "DAY",
            "AY" or "MON" => "MON",
            _ => "C62" // Varsayılan: Adet
        };
    }
}
