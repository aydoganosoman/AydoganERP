namespace AydoganERP.EInvoice.Abstractions.Enums;

/// <summary>
/// Fatura tipi
/// </summary>
public enum EInvoiceType
{
    /// <summary>Satış Faturası</summary>
    Sales = 0,

    /// <summary>İade Faturası</summary>
    Return = 1,

    /// <summary>Tevkifat Faturası</summary>
    Withholding = 2,

    /// <summary>İstisna Faturası</summary>
    Exception = 3,

    /// <summary>Özel Matrah Faturası</summary>
    SpecialBase = 4,

    /// <summary>SGK Faturası</summary>
    SGK = 5
}
