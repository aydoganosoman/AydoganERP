namespace AydoganERP.EInvoice.Abstractions.Models.Settings;

/// <summary>
/// Entegratör ayarları interface'i
/// </summary>
public interface IIntegratorSettings
{
    /// <summary>API URL</summary>
    string Url { get; set; }

    /// <summary>Kullanıcı adı</summary>
    string UserName { get; set; }

    /// <summary>Şifre</summary>
    string Password { get; set; }
}
