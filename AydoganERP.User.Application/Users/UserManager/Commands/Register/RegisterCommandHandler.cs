using AydoganERP.Base.Application.EMail;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Enums;
using AydoganERP.User.Application.Common.Repositories;
using AydoganERP.User.Domain.Entities;
using AydoganERP.User.Domain.Rules;
using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMD5Helper _md5Helper;
    private readonly IUserUniquenessChecker _userUniquenessChecker;
    private readonly IGeneratePasswordUtil _generatePasswordUtil;
    private readonly IEmailSender _emailSender;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public RegisterCommandHandler(IUserRepository userRepository,
        ICompanyRepository companyRepository,
        IMD5Helper md5Helper,
        IUserUniquenessChecker userUniquenessChecker,
        IGeneratePasswordUtil generatePasswordUtil,
        IEmailSender emailSender,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _userRepository = userRepository;
        _companyRepository = companyRepository;
        _md5Helper = md5Helper;
        _userUniquenessChecker = userUniquenessChecker;
        _generatePasswordUtil = generatePasswordUtil;
        _emailSender = emailSender;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User newUser = Domain.Entities.User.Register(_userUniquenessChecker,
            _generatePasswordUtil,
            $"{request.FirstName} {request.LastName}",
            request.Email,
            _md5Helper.GenerateMD5($"<<{request.Password}>>"));

        Company newCompany = newUser.CreateUserCompany();

        UserToRole newUserRole = newUser.CreateUserRole(newCompany, Domain.Enums.RoleEnum.AccountAdmin);

        newUserRole.AddUserAuthorization(ModuleEnum.InventoryModule, AuthorizationTransactionTypeEnum.Full);
        newUserRole.AddUserAuthorization(ModuleEnum.RevenueModule, AuthorizationTransactionTypeEnum.Full);
        newUserRole.AddUserAuthorization(ModuleEnum.CustomerModule, AuthorizationTransactionTypeEnum.Full);
        newUserRole.AddUserAuthorization(ModuleEnum.CashModule, AuthorizationTransactionTypeEnum.Full);
        newUserRole.AddUserAuthorization(ModuleEnum.CompanySettingsModule, AuthorizationTransactionTypeEnum.Full);
        newUserRole.AddUserAuthorization(ModuleEnum.EmployeeModule, AuthorizationTransactionTypeEnum.Full);
        newUserRole.AddUserAuthorization(ModuleEnum.SalesInvoiceModule, AuthorizationTransactionTypeEnum.Full);

        newCompany.AddAccessibleUser(newUserRole);

        await _userRepository.AddAsync(newUser, cancellationToken);
        await _companyRepository.AddAsync(newCompany, cancellationToken);

        int count = await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        if (count > 0)
            await _emailSender.SendEmailAsync(new EmailMessage(new string[] { request.Email }, "Eposta Onayı!", ""), null);

        return newUser.Id;
    }
}
