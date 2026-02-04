using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Base.Domain.Modules.IdentityModule.Enums;
using AydoganERP.Base.Domain.Modules.IdentityModule.Rules;
using AydoganERP.Identity.Application.Models;
using MediatR;

namespace AydoganERP.Identity.Application.UserManager.Commands.Register;

public record RegisterCompanyCommand(string Name, string Email) : IRequest<UserDto>;

public class RegisterCompanyCommandHandler : IRequestHandler<RegisterCompanyCommand, UserDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMD5Helper _md5Helper;
    private readonly IUserUniquenessChecker _userUniquenessChecker;
    private readonly IGeneratePasswordUtil _generatePasswordUtil;
    private readonly IMapper _mapper;

    public RegisterCompanyCommandHandler(IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMD5Helper md5Helper,
        IUserUniquenessChecker userUniquenessChecker,
        IGeneratePasswordUtil generatePasswordUtil,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _md5Helper = md5Helper;
        _userUniquenessChecker = userUniquenessChecker;
        _generatePasswordUtil = generatePasswordUtil;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
    {
        var generatedPassword = _generatePasswordUtil
            .CreateRandomPassword(8);
        
        var generatedPasswordSalted = $"<<{generatedPassword}>>";
        
        var generatedPasswordHashed = _md5Helper
            .GenerateMD5(generatedPasswordSalted);

        User newUser = User.Register(_userUniquenessChecker,
            _generatePasswordUtil,
            UserRoleEnum.CompanyAdmin,
            request.Name,
            request.Email,
            generatedPasswordSalted,
            generatedPasswordHashed);

        await _baseDbContext.Users.AddAsync(newUser, cancellationToken: cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<UserDto>(newUser);
    }
}