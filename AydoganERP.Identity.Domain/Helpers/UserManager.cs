using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Identity.Domain.Exceptions;

namespace AydoganERP.Identity.Domain.Helpers;

public static class UserManager
{
    public static User TokenIsValid(this User user, string refreshToken)
    {
        if (user.RefreshToken != refreshToken)
            throw new TokenIsNotValidException("Token Is Not Valid");
        
        return user;    
    }
}