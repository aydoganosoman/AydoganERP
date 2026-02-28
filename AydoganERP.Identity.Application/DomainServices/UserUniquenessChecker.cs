using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.IdentityModule.Rules;

namespace AydoganERP.Identity.Application.DomainServices;

public class UserUniquenessChecker : IUserUniquenessChecker
{
    private readonly IBaseDbContext _baseDbContext;
    public UserUniquenessChecker(IBaseDbContext baseDbContext)
    {
        _baseDbContext = baseDbContext;
    }

    public bool IsUnique(string userEmail)
    {
        return !_baseDbContext
            .Users
            .Any(x => x.Email == userEmail);
    }
}
