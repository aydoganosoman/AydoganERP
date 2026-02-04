using System.Net.Mail;
using AydoganERP.Base.Application.Common.Interfaces;

namespace AydoganERP.Base.Application.Common.EMail;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message, AlternateView view = null, IBaseDbContext dbContext = null);
}
