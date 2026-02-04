namespace AydoganERP.Base.Application.Common.EMail;

public struct EmailMessage
{
    public string[] To { get; }
    public string[] Cc { get; }
    public string[] Bcc { get; }
    public string Title { get; }
    public string Content { get; }
    public string[]? FilePath { get; set; }

    public EmailMessage(string[] to,
        string[] cc,
        string[] bcc,
        string title,
        string content,
        string[] filePath = null)
    {
        this.To = to;
        this.Cc = cc;
        this.Bcc = bcc;
        this.Title = title;
        this.Content = content;
        this.FilePath = filePath;
    }
}
