using AydoganERP.Base.Application.Common.EMail;
using AydoganERP.Base.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
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

        // using (var client = new SmtpClient())
        // {
        //     try
        //     {
        //         client.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
        //         //client.Authenticate("resend", _emailSettings.Password);
        //         client.Authenticate(_emailSettings.From, _emailSettings.Password);
        //         
        //         string result = await client.SendAsync(mime_message);
        //         client.Disconnect(true);
        //
        //         foreach (var notificationLog in _notificationLogs)
        //         {
        //             notificationLog.UpdateResult(result, NotificationStatusEnum.Sent);
        //
        //             if (dbContext == null)
        //                 await _dbContext.NotificationLogs.AddAsync(notificationLog);
        //             else
        //                 await dbContext.NotificationLogs.AddAsync(notificationLog);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         foreach (var notificationLog in _notificationLogs)
        //         {
        //             notificationLog.UpdateResult(ex.Message, NotificationStatusEnum.Failed);
        //
        //             if (dbContext == null)
        //                 await _dbContext.NotificationLogs.AddAsync(notificationLog);
        //             else
        //                 await dbContext.NotificationLogs.AddAsync(notificationLog);
        //         }
        //     }
        // }
    }
}