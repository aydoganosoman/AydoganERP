using AydoganERP.Base.Domain.Modules.IdentityModule.Rules;
using AydoganERP.Identity.Application.Repositories;

namespace AydoganERP.Identity.Application.DomainServices;

public class UserUniquenessChecker : IUserUniquenessChecker
{
    private readonly IUserRepository _userRepository;
    public UserUniquenessChecker(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsUnique(string userEmail)
    {
        return !_userRepository
            .GetDbContext()
            .Set<Base.Domain.Modules.IdentityModule.Entities.User>().Any(x => x.Email == userEmail);
    }
}
