namespace AydoganERP.EInvoice.Abstractions.Models.Settings;

/// <summary>
/// MySoft entegratör ayarları
/// </summary>
public class MySoftSettings : IIntegratorSettings
{
    /// <summary>API URL (örn: https://edocumentapi.mysoft.com.tr)</summary>
    public string Url { get; set; } = "https://edocumentapi.mysoft.com.tr";

    /// <summary>Kullanıcı adı</summary>
    public string UserName { get; set; } = default!;

    /// <summary>Şifre</summary>
    public string Password { get; set; } = default!;

    /// <summary>Connector GUID</summary>
    public string? ConnectorGuid { get; set; }

    /// <summary>Test modu mu?</summary>
    public bool IsTestMode { get; set; }
}
