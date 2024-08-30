using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Domain.Enums;
using AydoganERP.User.Application.Common.Repositories;
using AydoganERP.User.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Commands.AddAccessibleUser;

public class AddAccessibleUserCommandHandler : IRequestHandler<AddAccessibleUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserToRoleRepository _userToRoleRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public AddAccessibleUserCommandHandler(IUserRepository userRepository,
        IUserToRoleRepository userToRoleRepository,
        ICompanyRepository companyRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _userToRoleRepository = userToRoleRepository;
        _companyRepository = companyRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task Handle(AddAccessibleUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User user = await _userRepository
             .GetAsync(x => x.EMail == request.UserEmail);

        Company company = await _companyRepository
             .GetAsync(x => x.Id == _currentUserService.WorkingCompanyId);

        UserToRole newUserRole = UserToRole.Create(company, user, Domain.Enums.RoleEnum.User);

        newUserRole.AddUserAuthorization(ModuleEnum.InventoryModule, AuthorizationTransactionTypeEnum.None);
        newUserRole.AddUserAuthorization(ModuleEnum.RevenueModule, AuthorizationTransactionTypeEnum.None);
        newUserRole.AddUserAuthorization(ModuleEnum.CustomerModule, AuthorizationTransactionTypeEnum.None);
        newUserRole.AddUserAuthorization(ModuleEnum.CashModule,AuthorizationTransactionTypeEnum.None);
        newUserRole.AddUserAuthorization(ModuleEnum.CompanySettingsModule, AuthorizationTransactionTypeEnum.None);
        newUserRole.AddUserAuthorization(ModuleEnum.EmployeeModule, AuthorizationTransactionTypeEnum.None);
        newUserRole.AddUserAuthorization(ModuleEnum.SalesInvoiceModule, AuthorizationTransactionTypeEnum.None);

        await _userToRoleRepository.AddAsync(newUserRole, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync();
    }
}
