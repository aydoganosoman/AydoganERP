namespace AydoganERP.Base.Domain.Modules.FinanceModule.Enums;

/// <summary>
/// KDV dahil/hariç durumu
/// </summary>
public static class VatStatusEnum
{
    public const int Excluded = 0;  // KDV Hariç
    public const int Included = 1;  // KDV Dahil
}
