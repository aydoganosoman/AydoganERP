namespace AydoganERP.Base.Application.EMail;

public struct EmailMessage
{
    public string[] To { get; }
    public string Title { get; }
    public string Content { get; }
    public string[]? FilePath { get; set; }

    public EmailMessage(
        string[] to,
        string title,
        string content,
        string[] filePath = null)
    {
        this.To = to;
        this.Title = title;
        this.Content = content;
        this.FilePath = filePath;
    }
}
