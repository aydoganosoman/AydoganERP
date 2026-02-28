using AydoganERP.Base.Application.Common.EMail;
using AydoganERP.Base.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using EmailMessage = AydoganERP.Base.Application.Common.EMail.EmailMessage;

namespace AydoganERP.Base.Infrastructure.Email;

//test-api-key: re_buDqhq9R_PaEimzyQv37KnMryrwBiHt6G
public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly IBaseDbContext _dbContext;

    public EmailSender(IOptions<EmailSettings> emailSettings,
        IBaseDbContext dbContext)
    {
        _emailSettings = emailSettings.Value;
        _dbContext = dbContext;
    }

    public async Task SendEmailAsync(EmailMessage message, System.Net.Mail.AlternateView view = null,
        IBaseDbContext dbContext = null)
    {
        if (_emailSettings.Host == null)
            return;

        MailMessage _message = new MailMessage();
        _message.From = new MailAddress(_emailSettings.From);

        foreach (string se in message.To)
        {
            _message.To.Add(new MailAddress(se));
        }

        _message.Subject = message.Title;
        _message.Body = message.Content;

        if (message.FilePath != null)
            foreach (string se in message.FilePath)
            {
                if (!string.IsNullOrEmpty(se))
                    _message.Attachments.Add(new Attachment(se));
            }

        using (var client = new SmtpClient())
        {
            try
            {
                client.Host = _emailSettings.Host;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = _emailSettings.EnableSsl;
                
                if (_emailSettings.Port != 0)
                    client.Port = Convert.ToInt32(_emailSettings.Port);

                if (!string.IsNullOrEmpty(_emailSettings.SMTPUser))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_emailSettings.SMTPUser, _emailSettings.Password);
                }

                await client.SendMailAsync(_message);
            }
            catch (Exception ex)
            {
            }
        }
    }
}