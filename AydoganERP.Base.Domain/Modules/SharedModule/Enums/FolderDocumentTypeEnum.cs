namespace AydoganERP.Base.Domain.Modules.SharedModule.Enums;

/// <summary>
/// Klasör belge tipleri
/// </summary>
[Flags]
public enum FolderDocumentTypeEnum
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
    public static string GetName(FolderDocumentTypeEnum value)
    {
        return value switch
        {
            FolderDocumentTypeEnum.OutgoingEInvoice => "E-Fatura Giden",
            FolderDocumentTypeEnum.IncomingEInvoice => "E-Fatura Gelen",
            FolderDocumentTypeEnum.EArchiveInvoice => "E-Arşiv Fatura",
            FolderDocumentTypeEnum.OutgoingEWaybill => "E-İrsaliye Giden",
            FolderDocumentTypeEnum.IncomingEWaybill => "E-İrsaliye Gelen",
            FolderDocumentTypeEnum.EProducerReceipt => "E-Müstahsil Makbuzu",
            FolderDocumentTypeEnum.ESelfEmployment => "E-Serbest Meslek Makbuzu",
            _ => value.ToString()
        };
    }

    public static List<string> GetNames(FolderDocumentTypeEnum value)
    {
        var names = new List<string>();
        if (value.HasFlag(FolderDocumentTypeEnum.OutgoingEInvoice)) names.Add("E-Fatura Giden");
        if (value.HasFlag(FolderDocumentTypeEnum.IncomingEInvoice)) names.Add("E-Fatura Gelen");
        if (value.HasFlag(FolderDocumentTypeEnum.EArchiveInvoice)) names.Add("E-Arşiv Fatura");
        if (value.HasFlag(FolderDocumentTypeEnum.OutgoingEWaybill)) names.Add("E-İrsaliye Giden");
        if (value.HasFlag(FolderDocumentTypeEnum.IncomingEWaybill)) names.Add("E-İrsaliye Gelen");
        if (value.HasFlag(FolderDocumentTypeEnum.EProducerReceipt)) names.Add("E-Müstahsil Makbuzu");
        if (value.HasFlag(FolderDocumentTypeEnum.ESelfEmployment)) names.Add("E-Serbest Meslek Makbuzu");
        return names;
    }

    public static FolderDocumentTypeEnum Parse(string commaSeparatedValues)
    {
        if (string.IsNullOrWhiteSpace(commaSeparatedValues))
            return FolderDocumentTypeEnum.None;

        var result = FolderDocumentTypeEnum.None;
        var values = commaSeparatedValues.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        
        foreach (var val in values)
        {
            if (Enum.TryParse<FolderDocumentTypeEnum>(val, true, out var parsed))
                result |= parsed;
        }
        
        return result;
    }

    public static string ToCommaSeparated(FolderDocumentTypeEnum value)
    {
        if (value == FolderDocumentTypeEnum.None)
            return string.Empty;

        var names = new List<string>();
        foreach (FolderDocumentTypeEnum docType in Enum.GetValues<FolderDocumentTypeEnum>())
        {
            if (docType != FolderDocumentTypeEnum.None && docType != FolderDocumentTypeEnum.All && value.HasFlag(docType))
                names.Add(docType.ToString());
        }
        
        return string.Join(",", names);
    }
}
