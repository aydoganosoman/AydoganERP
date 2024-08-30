using AydoganERP.Base.Domain.Common;

namespace AydoganERP.User.Domain.Rules;

public class UserEmailMustBeUniqueRule : IBusinessRule
{
    private readonly IUserUniquenessChecker _userUniquenessChecker;
    private readonly string _email;

    public UserEmailMustBeUniqueRule(
        IUserUniquenessChecker userUniquenessChecker,
        string email)
    {
        _userUniquenessChecker = userUniquenessChecker;
        _email = email;
    }

    public string Title => "SameEmail";
    public string Message => "Bu Eposta ile kayıtlı kullanıcı vardır.";

    public bool IsBroken() => !_userUniquenessChecker.IsUnique(_email);
}
