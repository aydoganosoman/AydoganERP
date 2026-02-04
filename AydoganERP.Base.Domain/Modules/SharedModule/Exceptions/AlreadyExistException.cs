using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Exceptions;

public class AlreadyExistException : DomainException
{
    public AlreadyExistException(string message)
        : base(message)
    {
        
    }
}