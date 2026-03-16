using AydoganERP.EInvoice.Abstractions.Enums;
using AydoganERP.EInvoice.Abstractions.Models;
using AydoganERP.EInvoice.MySoft.Models.Request;
using AydoganERP.EInvoice.MySoft.Models.Response;
using AydoganERP.EInvoice.MySoft.Services;

namespace AydoganERP.EInvoice.MySoft.Mappers;

/// <summary>
/// MySoft model dönüşümleri
/// </summary>
public static class MySoftMapper
{
    public static GibAccount ToGibAccount(GibAccountResponse response)
    {
        var account = new GibAccount
        {
            TaxNumber = response.VknTckn ?? string.Empty,
            Title = response.Title ?? string.Empty,
            IsEInvoiceUser = response.IsEInvoiceUser,
            IsEArchiveUser = response.IsEArchiveUser,
            IsEWaybillUser = response.IsEWaybillUser
        };

        if (response.Aliases != null)
        {
            foreach (var alias in response.Aliases)
            {
                if (alias.Type == "PK")
                {
                    account.PkAliases.Add(alias.Alias ?? string.Empty);
                    account.PkAlias ??= alias.Alias;
                }
                else if (alias.Type == "GB")
                {
                    account.GbAliases.Add(alias.Alias ?? string.Empty);
                    account.GbAlias ??= alias.Alias;
                }
            }
        }

        if (DateTime.TryParse(response.FirstCreationTime, out var registerDate))
        {
            account.RegisterDate = registerDate;
        }

        return account;
    }

    public static InvoiceOutboxRequest ToInvoiceOutboxRequest(EInvoiceRequest request, string? connectorGuid)
    {
        var mySoftRequest = new InvoiceOutboxRequest
        {
            ConnectorGuid = connectorGuid,
            EDocumentType = (int)request.DocumentType,
            Profile = (int)request.Scenario,
            InvoiceType = (int)request.InvoiceType,
            Ettn = request.ETTN,
            Prefix = request.Prefix,
            DocNo = request.InvoiceNumber,
            DocDate = request.InvoiceDate.ToString("MM/dd/yyyy"),
            DocTime = request.InvoiceTime.ToString(@"hh\:mm\:ss"),
            CurrencyCode = request.Currency.ToString(),
            CurrencyRate = (double)request.ExchangeRate,
            SenderType = request.DocumentType == EInvoiceDocumentType.EArchive ? "ELEKTRONIK" : null,
            PkAlias = request.ReceiverPkAlias,
            GbAlias = request.ReceiverGbAlias,
            TenantIdentifierNumber = request.SenderTaxNumber,
            ReferanceKey = request.InvoiceId,
            IsCalculateByApi = false,
            IsManuelCalculation = true,
            isAddPayableAmountString = true,
            InvoiceAccount = new InvoiceAccountModel
            {
                VknTckn = request.ReceiverTaxNumber,
                AccountName = request.ReceiverTitle,
                TaxOfficeName = request.ReceiverTaxOffice,
                CountryName = "TÜRKİYE",
                CityName = request.ReceiverCity,
                CitySubdivision = request.ReceiverDistrict,
                StreetName = request.ReceiverAddress,
                Email1 = request.ReceiverEmail
            },
            InvoiceCalculation = new InvoiceCalculationModel
            {
                LineExtensionAmount = (double)request.SubTotal,
                TaxExclusiveAmount = (double)request.TaxableAmount,
                TaxInclusiveAmount = (double)request.GrandTotal,
                PayableAmount = (double)request.PayableAmount,
                AllowanceTotalAmount = (double)request.DiscountTotal
            }
        };

        // İade fatura bilgileri
        if (request.InvoiceType == EInvoiceType.Return && !string.IsNullOrEmpty(request.ReturnInvoiceNumber))
        {
            mySoftRequest.BillingRefInvoiceNo = request.ReturnInvoiceNumber;
            mySoftRequest.BillingRefInvoiceDate = request.ReturnInvoiceDate?.ToString("MM/dd/yyyy HH:mm:ss");
        }

        // Sipariş bilgisi
        if (!string.IsNullOrEmpty(request.OrderNumber))
        {
            mySoftRequest.OrderNo = request.OrderNumber;
            mySoftRequest.OrderDate = request.OrderDate?.ToString("MM/dd/yyyy");
        }

        // İrsaliye bilgisi
        if (!string.IsNullOrEmpty(request.WaybillNumber))
        {
            mySoftRequest.WaybillInfo = new List<WaybillInfoModel>
            {
                new WaybillInfoModel
                {
                    WaybillNo = request.WaybillNumber,
                    WaybillDate = request.WaybillDate?.ToString("MM/dd/yyyy")
                }
            };
        }

        // Notlar
        if (!string.IsNullOrEmpty(request.Description))
        {
            mySoftRequest.Notes.Add(new NoteModel { Note = request.Description });
        }
        foreach (var note in request.Notes)
        {
            mySoftRequest.Notes.Add(new NoteModel { Note = note });
        }

        // Satırlar
        foreach (var line in request.Lines)
        {
            var detail = new InvoiceDetailModel
            {
                ProductCode = line.ProductCode,
                ProductName = !string.IsNullOrEmpty(line.Description) ? line.Description : line.ProductName,
                UnitCode = line.UnitCode,
                Qty = (double)line.Quantity,
                UnitPriceTra = (double)line.UnitPrice,
                AmtTra = (double)line.LineTotal,
                VatRate = (double)line.VatRate,
                AmtVatTra = (double)line.VatAmount,
                TaxableAmtTra = (double)(line.LineTotal - line.DiscountAmount),
                Note = line.Description,
                TaxExemptionReasonCode = line.VatExemptionCode,
                TaxExemptionReasonName = line.VatExemptionReason,
                IsSubtractDiscountFromAmtTra = true
            };

            // Tevkifat
            if (!string.IsNullOrEmpty(line.WithholdingTaxCode))
            {
                detail.WithholdingTaxTypeCode = line.WithholdingTaxCode;
                detail.WithholdingTaxTypeName = line.WithholdingTaxName;
                detail.WithholdingTaxPercentage = (int)(line.WithholdingTaxRate ?? 0);
                detail.WithholdingTaxableAmount = line.VatAmount;
                detail.WithholdingTaxAmount = line.WithholdingTaxAmount ?? 0;
            }

            // İskonto
            if (line.DiscountAmount > 0)
            {
                detail.AllowanceCharge.Add(new AllowanceChargeModel
                {
                    ChargeIndicator = false,
                    MultiplierFactorNumeric = (double)(line.DiscountRate / 100),
                    SequenceNumeric = 1,
                    Amount = (double)line.DiscountAmount,
                    BaseAmount = (double)line.LineTotal
                });
            }

            detail.ItemInstance.Add(new ItemInstanceModel { SerialId = line.ProductCode });

            mySoftRequest.InvoiceDetail.Add(detail);
        }

        // Kamu faturası için banka bilgileri
        if (request.Scenario == EInvoiceScenario.Public && !string.IsNullOrEmpty(request.BankIBAN))
        {
            mySoftRequest.PaymentMeans = new List<PaymentMeansModel>
            {
                new PaymentMeansModel
                {
                    PaymentMeansCode = "42",
                    PaymentChannelCode = "BANKA",
                    PayeeFinancialAccount = new FinancialAccountModel
                    {
                        CurrencyCode = request.Currency.ToString(),
                        ID = request.BankIBAN
                    }
                }
            };
            mySoftRequest.PublicServicePayeeVKN = request.ReceiverTaxNumber;
            mySoftRequest.PublicServicePayeePartyName = request.ReceiverTitle;
            mySoftRequest.PublicServicePayeeCountry = "TÜRKİYE";
            mySoftRequest.PublicServicePayeeCity = request.ReceiverCity;
            mySoftRequest.PublicServicePayeeCitysubdivision = request.ReceiverDistrict;
        }

        return mySoftRequest;
    }

    public static IncomingInvoice ToIncomingInvoice(IncomingInvoiceResponse response)
    {
        return new IncomingInvoice
        {
            IntegratorId = response.Id.ToString(),
            ETTN = response.Ettn ?? string.Empty,
            InvoiceNumber = response.DocNo ?? string.Empty,
            InvoiceDate = DateTime.TryParse(response.DocDate, out var date) ? date : DateTime.MinValue,
            DocumentType = EInvoiceDocumentType.EInvoice,
            Scenario = (EInvoiceScenario)response.Profile,
            InvoiceType = (EInvoiceType)response.InvoiceType,
            Status = ParseInvoiceStatus(response.InvoiceStatusText),
            SenderTaxNumber = response.VknTckn ?? string.Empty,
            SenderTitle = response.AccountName ?? string.Empty,
            SenderPkAlias = response.PkAlias,
            SenderGbAlias = response.GbAlias,
            Currency = Enum.TryParse<CurrencyType>(response.CurrencyCode, out var currency) ? currency : CurrencyType.TRY,
            ExchangeRate = response.CurrencyRate,
            SubTotal = response.LineExtensionAmount,
            TaxableAmount = response.TaxExclusiveAmount,
            VatTotal = response.TaxTotalTra,
            DiscountTotal = response.AllowanceTotalAmount,
            GrandTotal = response.TaxInclusiveAmount,
            PayableAmount = response.PayableAmount,
            ReceivedAt = response.CreateDate,
            ReferenceKey = response.ReferanceKey
        };
    }

    public static EInvoiceStatusResult ToStatusResult(InvoiceStatusResponse response)
    {
        return new EInvoiceStatusResult
        {
            Success = true,
            ETTN = response.InvoiceETTN,
            InvoiceNumber = response.DocNo,
            Status = ParseInvoiceStatus(response.InvoiceStatusText),
            StatusDescription = response.InvoiceStatusText,
            DeclineReason = response.DeclineReason,
            EnvelopeId = response.EnvelopeIdentifier,
            EnvelopeStatus = response.EnvelopeStatusText,
            GibEnvelopeStatusCode = response.GibEnvelopeStatusCode,
            TryCount = response.TryCount,
            GTBRefNo = response.GTBRefNo,
            GTBRegistryNo = response.GTBGCBRegistryNo,
            GTBExportDate = DateTime.TryParse(response.GTBActualExportDate, out var gtbDate) ? gtbDate : null
        };
    }

    private static EInvoiceStatus ParseInvoiceStatus(string? statusText)
    {
        return statusText?.ToLowerInvariant() switch
        {
            "taslak" => EInvoiceStatus.Draft,
            "gönderiliyor" or "sending" => EInvoiceStatus.Sending,
            "gönderildi" or "sent" => EInvoiceStatus.Sent,
            "kabul edildi" or "accepted" => EInvoiceStatus.Accepted,
            "reddedildi" or "declined" or "rejected" => EInvoiceStatus.Rejected,
            "iptal edildi" or "cancelled" => EInvoiceStatus.Cancelled,
            "beklemede" or "pending" => EInvoiceStatus.Pending,
            "hata" or "error" => EInvoiceStatus.Error,
            _ => EInvoiceStatus.Draft
        };
    }
}
