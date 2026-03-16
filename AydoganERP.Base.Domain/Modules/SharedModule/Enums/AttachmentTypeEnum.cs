namespace AydoganERP.Base.Domain.Modules.SharedModule.Enums;

/// <summary>
/// Doküman eki tipleri - hangi modüle/entity'ye ait olduğunu belirtir
/// </summary>
public static class AttachmentTypeEnum
{
    public const int Invoice = 1;       // Fatura ekleri
    public const int Order = 2;         // Sipariş dokümanları
    public const int Waybill = 3;       // İrsaliye dokümanları
    public const int Party = 4;         // Müşteri/Tedarikçi dokümanları
    public const int Product = 5;       // Ürün dokümanları
    public const int Contract = 6;      // Sözleşmeler
    public const int Other = 99;        // Diğer
}
