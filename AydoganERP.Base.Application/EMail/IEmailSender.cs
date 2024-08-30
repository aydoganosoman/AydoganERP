using System.Net.Mail;

namespace AydoganERP.Base.Application.EMail;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message, AlternateView view = null);
}
