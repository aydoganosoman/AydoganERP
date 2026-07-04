using AydoganERP.EInvoice.Abstractions.Enums;
using AydoganERP.EInvoice.Abstractions.Models;
using AydoganERP.EInvoice.Bien.Integration;

namespace AydoganERP.EInvoice.Bien.Services;

/// <summary>
/// Bien SOAP tipleri ile Abstractions modelleri arasında dönüşüm yapar
/// </summary>
public static class BienMapper
{
    #region GibAccount Mapping

    /// <summary>
    /// TurmobResponse'dan GibAccount'a dönüşüm
    /// </summary>
    public static GibAccount ToGibAccount(TurmobResponse source)
    {
        return new GibAccount
        {
            TaxNumber = source.VKN ?? source.SorguKimlikNO ?? string.Empty,
            Title = !string.IsNullOrEmpty(source.Unvan)
                ? source.Unvan
                : $"{source.Adi} {source.Soyadi}".Trim(),
            IsEInvoiceUser = true, // Turmob sorgusu döndüyse mükelleftir
            IsEArchiveUser = false, // Bu bilgi Turmob'dan gelmiyor
            IsEWaybillUser = false,
            PkAlias = null, // Turmob'dan PK/GB gelmez
            GbAlias = null
        };
    }

    #endregion

    #region Status Mapping

    /// <summary>
    /// Bien InvoiceStatus'tan EInvoiceStatus'a dönüşüm
    /// </summary>
    public static EInvoiceStatus ToEInvoiceStatus(InvoiceStatus source)
    {
        return source switch
        {
            InvoiceStatus.Draft => EInvoiceStatus.Draft,
            InvoiceStatus.NotPrepared => EInvoiceStatus.Draft,
            InvoiceStatus.NotSend => EInvoiceStatus.Draft,
            InvoiceStatus.Queued => EInvoiceStatus.Queued,
            InvoiceStatus.Processing => EInvoiceStatus.Sending,
            InvoiceStatus.SentToGib => EInvoiceStatus.SentToGIB,
            InvoiceStatus.Approved => EInvoiceStatus.Accepted,
            InvoiceStatus.WaitingForAprovement => EInvoiceStatus.WaitingResponse,
            InvoiceStatus.Declined => EInvoiceStatus.Rejected,
            InvoiceStatus.Return => EInvoiceStatus.Rejected,
            InvoiceStatus.Canceled => EInvoiceStatus.Cancelled,
            InvoiceStatus.EArchivedCanceled => EInvoiceStatus.Cancelled,
            InvoiceStatus.Error => EInvoiceStatus.Error,
            _ => EInvoiceStatus.Error
        };
    }

    /// <summary>
    /// InvoiceStatusInfo'dan EInvoiceStatusResult'a dönüşüm
    /// </summary>
    public static EInvoiceStatusResult ToEInvoiceStatusResult(InvoiceStatusInfo source)
    {
        return new EInvoiceStatusResult
        {
            Success = true,
            ETTN = source.InvoiceId,
            Status = ToEInvoiceStatus(source.Status),
            StatusDescription = source.Message ?? source.Status.ToString(),
            ErrorMessage = source.Status == InvoiceStatus.Error ? source.Message : null
        };
    }

    #endregion

    #region IncomingInvoice Mapping

    /// <summary>
    /// InboxInvoiceListItem'dan IncomingInvoice'a dönüşüm
    /// </summary>
    public static IncomingInvoice ToIncomingInvoice(InboxInvoiceListItem source)
    {
        return new IncomingInvoice
        {
            IntegratorId = source.DocumentId ?? source.InvoiceId,
            ETTN = source.InvoiceId,
            InvoiceNumber = source.InvoiceId, // InvoiceNumber ayrı bir property değil
            InvoiceDate = source.ExecutionDate ?? source.CreateDateUtc,
            DocumentType = EInvoiceDocumentType.EInvoice,
            Scenario = ToEInvoiceScenario(source.Status),
            InvoiceType = ToEInvoiceType(source.Type),
            Status = ToEInvoiceStatus(source.Status),
            SenderTaxNumber = source.TargetTcknVkn ?? string.Empty,
            SenderTitle = source.TargetTitle ?? string.Empty,
            Currency = ParseCurrency(source.DocumentCurrencyCode),
            ExchangeRate = source.ExchangeRate,
            SubTotal = source.TaxExclusiveAmount,
            TaxableAmount = source.TaxExclusiveAmount,
            VatTotal = source.TaxTotal,
            DiscountTotal = 0,
            GrandTotal = source.PayableAmount,
            PayableAmount = source.PayableAmount,
            ReceivedAt = source.CreateDateUtc
        };
    }

    #endregion

    #region Enum Mappings

    /// <summary>
    /// Para birimi dönüşümü
    /// </summary>
    public static CurrencyType ParseCurrency(string? currencyCode)
    {
        return currencyCode?.ToUpperInvariant() switch
        {
            "TRY" or "TRL" => CurrencyType.TRY,
            "USD" => CurrencyType.USD,
            "EUR" => CurrencyType.EUR,
            "GBP" => CurrencyType.GBP,
            _ => CurrencyType.TRY
        };
    }

    /// <summary>
    /// InvoiceStatus'tan EInvoiceScenario tahmini
    /// </summary>
    public static EInvoiceScenario ToEInvoiceScenario(InvoiceStatus status)
    {
        // Bien'de scenario ayrı gelmiyor, varsayılan Basic
        return status == InvoiceStatus.WaitingForAprovement
            ? EInvoiceScenario.Commercial
            : EInvoiceScenario.Basic;
    }

    /// <summary>
    /// InvoiceTypes'tan EInvoiceType'a dönüşüm
    /// NOT: InvoiceTypes aslında fatura senaryosunu (scenario) temsil ediyor,
    /// fatura tipini (Sales/Return) ise InvoiceTipType temsil ediyor.
    /// InboxInvoiceListItem.Type property'si InvoiceTypes kullanıyor.
    /// </summary>
    public static EInvoiceType ToEInvoiceType(InvoiceTypes source)
    {
        // InvoiceTypes fatura senaryosu, varsayılan satış döndür
        return EInvoiceType.Sales;
    }

    /// <summary>
    /// InvoiceTipType'tan EInvoiceType'a dönüşüm
    /// </summary>
    public static EInvoiceType ToEInvoiceType(InvoiceTipType source)
    {
        return source switch
        {
            InvoiceTipType.Sales => EInvoiceType.Sales,
            InvoiceTipType.Return => EInvoiceType.Return,
            InvoiceTipType.Exception => EInvoiceType.Exception,
            InvoiceTipType.Tax => EInvoiceType.Sales,
            InvoiceTipType.Sgk => EInvoiceType.SGK,
            InvoiceTipType.WithholdingReturn => EInvoiceType.Withholding,
            _ => EInvoiceType.Sales
        };
    }

    /// <summary>
    /// EInvoiceScenario'dan InvoiceScenarioChoosen'a dönüşüm
    /// </summary>
    public static InvoiceScenarioChoosen ToInvoiceScenarioChoosen(EInvoiceScenario scenario, EInvoiceDocumentType docType)
    {
        if (docType == EInvoiceDocumentType.EArchive)
            return InvoiceScenarioChoosen.eArchive;

        return scenario switch
        {
            EInvoiceScenario.Basic => InvoiceScenarioChoosen.eInvoice,
            EInvoiceScenario.Commercial => InvoiceScenarioChoosen.eInvoice,
            EInvoiceScenario.Export => InvoiceScenarioChoosen.eInvoice,
            EInvoiceScenario.Public => InvoiceScenarioChoosen.eInvoice,
            _ => InvoiceScenarioChoosen.Automated
        };
    }

    /// <summary>
    /// EInvoiceType'tan InvoiceTipType'a dönüşüm
    /// </summary>
    public static InvoiceTipType ToInvoiceTipType(EInvoiceType type)
    {
        return type switch
        {
            EInvoiceType.Sales => InvoiceTipType.Sales,
            EInvoiceType.Return => InvoiceTipType.Return,
            EInvoiceType.Withholding => InvoiceTipType.WithholdingReturn,
            EInvoiceType.Exception => InvoiceTipType.Exception,
            EInvoiceType.SGK => InvoiceTipType.Sgk,
            _ => InvoiceTipType.Sales
        };
    }

    #endregion
}
