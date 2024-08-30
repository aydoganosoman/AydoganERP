namespace AydoganERP.User.Domain.Rules;

public interface IUserUniquenessChecker
{
    bool IsUnique(string userEmail);
}
