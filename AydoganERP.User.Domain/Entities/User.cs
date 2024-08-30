using AydoganERP.Base.Domain.Common;
using AydoganERP.User.Domain.Enums;
using AydoganERP.User.Domain.Exceptions;
using AydoganERP.User.Domain.Rules;

namespace AydoganERP.User.Domain.Entities;

public class User : Entity<Guid>
{
    public User()
    {

    }

    private User(string name,
        string email,
        string password)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.EMail = email;
        this.Password = password;
        this.ApiKey = $"aydogancrm_{this.Id}";
        this.IsEnable = true;
    }

    private User(IUserUniquenessChecker userUniquenessChecker,
        IGeneratePasswordUtil generatePasswordUtil,
        string name,
        string email,
        string password)
    {
        CheckRule(new UserEmailMustBeUniqueRule(userUniquenessChecker, email));

        this.Id = Guid.NewGuid();
        this.Name = name;
        this.ProfilePicture = name;
        this.EMail = email;
        this.Password = password;
        this.IsEnable = true;
        this.ApiKey = $"aydogancrm_{generatePasswordUtil.CreateRandomPassword(7)}";
    }

    public string Name { get; private set; }
    public string EMail { get; private set; }
    public string? ProfilePicture { get; private set; }
    public string? Title { get; private set; }
    public string? Phone { get; private set; }
    public string Password { get; private set; }
    public string ApiKey { get; private set; }
    public string? RefreshToken { get; private set; }
    public bool Verification { get; private set; }
    public bool IsEnable { get; private set; }
    public bool IsDelete { get; private set; }

    public void Enable() => this.IsEnable = true;
    public void Disable() => this.IsEnable = false;
    public void Delete() => this.IsDelete = true;
    public void Verified() => this.Verification = true;
    public void Unverified() => this.Verification = false;
    public void UpdateRefreshToken(string refreshToken) => this.RefreshToken = refreshToken;
    public void TokenIsValid(string refreshToken)
    {
        if (this.RefreshToken != refreshToken)
            throw new TokenIsNotValidException("Token Is Not Valid");
    }
    public void RefreshPassword(IGeneratePasswordUtil generatePasswordUtil)
    {
        this.Password = $"{generatePasswordUtil.CreateRandomPassword(16)}";
    }

    public static User Create(string name,
        string email,
        string password)
    {
        return new User(name, email, password);
    }

    public static User Register(IUserUniquenessChecker userUniquenessChecker,
        IGeneratePasswordUtil generatePasswordUtil,
        string name,
        string email,
        string password)
    {
        return new User(userUniquenessChecker, generatePasswordUtil, name, email, password);
    }

    public UserToRole CreateUserRole(Company company, RoleEnum role)
    {
        return UserToRole.Create(company, this, role);
    }

    public Company CreateUserCompany()
    {
        return Company.Create(this.Name);
    }
}
