using AydoganERP.Base.Application.EMail;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AydoganERP.Customer.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    public async Task SendEmailAsync(EmailMessage message, AlternateView view = null)
    {
        MailMessage _message = new MailMessage();
        _message.From = new MailAddress(_emailSettings.From);

        message.To.ToList().ForEach(x =>
        {
            _message.To.Add(new MailAddress(x));
        });

        _message.Subject = message.Title;
        
        if (view != null)
            _message.AlternateViews.Add(view);
        else
            _message.Body = message.Content;

        message.FilePath.ToList().ForEach(x =>
        {
            if (!string.IsNullOrEmpty(x))
                _message.Attachments.Add(new Attachment(x));
        });

        SmtpClient client = new SmtpClient();
        client.Port = _emailSettings.Port;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_emailSettings.From, _emailSettings.Password);
        client.Host = _emailSettings.Host;
        client.EnableSsl = _emailSettings.EnableSsl;

        await client.SendMailAsync(_message);
    }
}
