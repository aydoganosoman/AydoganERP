namespace AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

/// <summary>
/// Alıcı/Satıcı numara tipleri
/// </summary>
public static class PartyNumberTypeEnum
{
    public const int SubscriberNo = 1;  // Abone Numarası
    public const int DealerNo = 2;      // Bayi Numarası
    public const int FarmerNo = 3;      // Çiftçi Numarası
    public const int TaxNo = 4;         // Vergi Kimlik Numarası (VKN)
    public const int IdNo = 5;          // TC Kimlik Numarası (TCKN)
    public const int EpdkNo = 6;        // EPDK Lisans Numarası
}
