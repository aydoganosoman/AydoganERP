using AydoganERP.Base.Application.EMail;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Domain.Common;
using AydoganERP.User.Application.Common.Repositories;
using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest
{
    public string Email { get; set; }
}

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IGeneratePasswordUtil _generatePasswordUtil;
    private readonly IEmailSender _emailSender;
    public ForgotPasswordCommandHandler(IUserRepository userRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IGeneratePasswordUtil generatePasswordUtil,
        IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _generatePasswordUtil = generatePasswordUtil;
        _emailSender = emailSender;
    }

    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User currentUser = await _userRepository
            .GetAsync(x => x.EMail == request.Email);

        if (currentUser == null)
            throw new Exception("User not found!");

        currentUser.RefreshPassword(_generatePasswordUtil);

        int count = await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        if (count > 0)
            await _emailSender.SendEmailAsync(new EmailMessage(new string[] { request.Email }, "Şifre Sıfırlama!", ""));
    }
}
