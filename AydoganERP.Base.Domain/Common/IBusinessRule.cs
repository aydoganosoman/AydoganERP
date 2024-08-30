namespace AydoganERP.Base.Domain.Common;

public interface IBusinessRule
{
    bool IsBroken();
    string Title { get; }
    string Message { get; }
}
