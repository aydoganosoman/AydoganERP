using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Identity.Domain.Exceptions;

public class TokenIsNotValidException : DomainException
{
    public TokenIsNotValidException(string message)
        : base(message)
    {
    }
}
