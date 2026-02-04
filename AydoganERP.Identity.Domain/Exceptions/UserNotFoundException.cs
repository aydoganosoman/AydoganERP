using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Identity.Domain.Exceptions;

public class UserNotFoundException : DomainException
{
    public UserNotFoundException(string message)
        : base(message)
    {
    }

}
