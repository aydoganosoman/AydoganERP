using AydoganERP.User.Application.Common.Repositories;
using AydoganERP.User.Domain.Rules;

namespace AydoganERP.User.Application.Users.DomainServices;

public class UserUniquenessChecker : IUserUniquenessChecker
{
    private readonly IUserRepository _userRepository;
    public UserUniquenessChecker(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsUnique(string userEmail)
    {
        return !_userRepository.Query().Any(x => x.EMail == userEmail);
    }
}
