namespace AydoganERP.Base.Domain.Modules.InventoryModule.Enums;

public enum SerialNumberStatusEnum
{
    InStock = 0,      // Stokta
    Sold = 1,         // Satıldı
    InService = 2,    // Serviste
    Returned = 3,     // İade edildi
    Defective = 4,    // Arızalı/Kusurlu
    Scrapped = 5      // Hurda
}
