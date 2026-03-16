namespace AydoganERP.EInvoice.Abstractions.Models.Settings;

/// <summary>
/// Bien entegratör ayarları
/// </summary>
public class BienSettings : IIntegratorSettings
{
    /// <summary>API URL (örn: https://connect.bienteknoloji.com.tr/Services)</summary>
    public string Url { get; set; } = "https://connect.bienteknoloji.com.tr/Services";

    /// <summary>Kullanıcı adı</summary>
    public string UserName { get; set; } = default!;

    /// <summary>Şifre</summary>
    public string Password { get; set; } = default!;

    /// <summary>Test modu mu?</summary>
    public bool IsTestMode { get; set; }
}
