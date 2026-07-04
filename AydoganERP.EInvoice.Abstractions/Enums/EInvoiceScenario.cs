namespace AydoganERP.EInvoice.Abstractions.Enums;

/// <summary>
/// E-Fatura senaryosu
/// </summary>
public enum EInvoiceScenario
{
    /// <summary>Temel Fatura</summary>
    Basic = 0,

    /// <summary>Ticari Fatura</summary>
    Commercial = 1,

    /// <summary>İhracat Faturası</summary>
    Export = 2,

    /// <summary>Yolcu Beraberi</summary>
    PassengerAccompanied = 3,

    /// <summary>Kamu</summary>
    Public = 4,

    /// <summary>Hal Tipi Fatura</summary>
    HallType = 5,

    /// <summary>E-Arşiv</summary>
    EArchive = 6,
    
    /// <summary>İstisna</summary>
    Except = 6
}
