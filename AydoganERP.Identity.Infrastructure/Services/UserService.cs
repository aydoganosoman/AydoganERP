using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Application.Common.Services;
using AydoganERP.Identity.Application.Repositories;

namespace AydoganERP.Identity.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository  _userRepository;
    private readonly ICurrentUserService _currentUserService;
    public UserService(IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }
    

}