namespace AydoganERP.Base.Domain.Modules.SharedModule.Enums;

/// <summary>
/// Klasör belge tipleri
/// </summary>
[Flags]
public enum DocumentTypeEnum
{
    None = 0,
    OutgoingEInvoice = 1,     // E-Fatura Giden
    IncomingEInvoice = 2,     // E-Fatura Gelen
    EArchiveInvoice = 4,      // E-Arşiv Fatura
    OutgoingEWaybill = 8,     // E-İrsaliye Giden
    IncomingEWaybill = 16,    // E-İrsaliye Gelen
    EProducerReceipt = 32,    // E-Müstahsil Makbuzu
    ESelfEmployment = 64,     // E-Serbest Meslek Makbuzu
    All = OutgoingEInvoice | IncomingEInvoice | EArchiveInvoice | 
          OutgoingEWaybill | IncomingEWaybill | EProducerReceipt | ESelfEmployment
}

public static class DocumentTypeEnumExtensions
{
    public static string GetName(DocumentTypeEnum value)
    {
        return value switch
        {
            DocumentTypeEnum.OutgoingEInvoice => "E-Fatura Giden",
            DocumentTypeEnum.IncomingEInvoice => "E-Fatura Gelen",
            DocumentTypeEnum.EArchiveInvoice => "E-Arşiv Fatura",
            DocumentTypeEnum.OutgoingEWaybill => "E-İrsaliye Giden",
            DocumentTypeEnum.IncomingEWaybill => "E-İrsaliye Gelen",
            DocumentTypeEnum.EProducerReceipt => "E-Müstahsil Makbuzu",
            DocumentTypeEnum.ESelfEmployment => "E-Serbest Meslek Makbuzu",
            _ => value.ToString()
        };
    }

    public static List<string> GetNames(DocumentTypeEnum value)
    {
        var names = new List<string>();
        if (value.HasFlag(DocumentTypeEnum.OutgoingEInvoice)) names.Add("E-Fatura Giden");
        if (value.HasFlag(DocumentTypeEnum.IncomingEInvoice)) names.Add("E-Fatura Gelen");
        if (value.HasFlag(DocumentTypeEnum.EArchiveInvoice)) names.Add("E-Arşiv Fatura");
        if (value.HasFlag(DocumentTypeEnum.OutgoingEWaybill)) names.Add("E-İrsaliye Giden");
        if (value.HasFlag(DocumentTypeEnum.IncomingEWaybill)) names.Add("E-İrsaliye Gelen");
        if (value.HasFlag(DocumentTypeEnum.EProducerReceipt)) names.Add("E-Müstahsil Makbuzu");
        if (value.HasFlag(DocumentTypeEnum.ESelfEmployment)) names.Add("E-Serbest Meslek Makbuzu");
        return names;
    }

    public static DocumentTypeEnum Parse(string commaSeparatedValues)
    {
        if (string.IsNullOrWhiteSpace(commaSeparatedValues))
            return DocumentTypeEnum.None;

        var result = DocumentTypeEnum.None;
        var values = commaSeparatedValues.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        
        foreach (var val in values)
        {
            if (Enum.TryParse<DocumentTypeEnum>(val, true, out var parsed))
                result |= parsed;
        }
        
        return result;
    }

    public static string ToCommaSeparated(DocumentTypeEnum value)
    {
        if (value == DocumentTypeEnum.None)
            return string.Empty;

        var names = new List<string>();
        foreach (DocumentTypeEnum docType in Enum.GetValues<DocumentTypeEnum>())
        {
            if (docType != DocumentTypeEnum.None && docType != DocumentTypeEnum.All && value.HasFlag(docType))
                names.Add(docType.ToString());
        }
        
        return string.Join(",", names);
    }
}
