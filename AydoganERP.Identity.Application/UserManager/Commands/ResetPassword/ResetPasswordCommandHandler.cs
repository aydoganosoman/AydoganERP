using AydoganERP.Base.Application.Common.EMail;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Identity.Application.UserManager.Commands.ResetPassword;

public record ResetPasswordCommand(string Email) : IRequest;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IGeneratePasswordUtil _generatePasswordUtil;
    private readonly IMD5Helper _md5Helper;
    private readonly IEmailSender _emailSender;

    public ResetPasswordCommandHandler(IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IGeneratePasswordUtil generatePasswordUtil,
        IMD5Helper md5Helper,
        IEmailSender emailSender)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _generatePasswordUtil = generatePasswordUtil;
        _md5Helper = md5Helper;
        _emailSender = emailSender;
    }

    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        User currentUser = await _baseDbContext
            .Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (currentUser == null)
            throw new Exception("User not found!");

        string mail_template = File.ReadAllText("MailSignature.html");

        string newPassword = _generatePasswordUtil.CreateRandomPassword(8);
        string newPasswordSalted = $"<<{newPassword}>>";
        string newPasswordHashed = _md5Helper.GenerateMD5(newPasswordSalted);

        currentUser.RefreshPassword(newPasswordHashed);

        int count = await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        mail_template = mail_template.Replace("@message", $"Yeni Şifre/New Password: {newPassword}")
            .Replace("@date", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        if (count > 0)
            await _emailSender.SendEmailAsync(new EmailMessage(new string[] { request.Email },
                null,
                null,
                "Şifre Sıfırlama!/Reset Password!",
                mail_template));
    }
}