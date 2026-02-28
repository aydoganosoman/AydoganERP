namespace AydoganERP.Base.Application.Common.EMail;

public class EmailSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string From { get; set; }
    public string SMTPUser { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
}
