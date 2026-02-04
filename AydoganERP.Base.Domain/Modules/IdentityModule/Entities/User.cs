using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.IdentityModule.Enums;
using AydoganERP.Base.Domain.Modules.IdentityModule.Events;
using AydoganERP.Base.Domain.Modules.IdentityModule.Rules;

namespace AydoganERP.Base.Domain.Modules.IdentityModule.Entities;

public class User : Entity
{
    //For EF
    public User() { }

    private User(IUserUniquenessChecker userUniquenessChecker,
        IGeneratePasswordUtil generatePasswordUtil,
        int role,
        string name,
        string email,
        string password,
        string hashPassword)
    {
        if (userUniquenessChecker != null)
            CheckRule(new UserEmailMustBeUniqueRule(userUniquenessChecker, email));

        this.Id = Guid.NewGuid();
        this.Role = role;
        this.Name = name.Trim();
        this.Email = email.Trim();
        this.Password = password;
        this.HashPassword = hashPassword;
        this.ForcePasswordChange = true;
        this.ApiKey = $"{generatePasswordUtil.CreateRandomPassword(16)}";
        
        this.PublishEvent(new UserCreatedEvent(this));
    }

    public Guid Id { get; private set; }
    public Guid? CompanyId { get; private set; }
    public Company? Company { get; private set; } = default!; 
    public int Role { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string? Title { get; private set; }
    public string Password { get; private set; }
    public string HashPassword { get; private set; }
    public string ApiKey { get; private set; }
    public string? RefreshToken { get; private set; }
    public bool ForcePasswordChange { get; private set; }
    public int Status { get; private set; } = UserStatusEnum.Active;
    
    public void Lock() => Status = UserStatusEnum.Locked;
    public void Activate() => Status = UserStatusEnum.Active;
    public void UpdateRefreshToken(string refreshToken) => this.RefreshToken = refreshToken;
    public void RefreshPassword(string password) => this.Password = password;
    public void RequirePasswordChange()=> this.ForcePasswordChange = true;
    public void ChangePassword(string password)
    {
        Guard.Against(this.Status != UserStatusEnum.Active, "Disabled user cannot change password.");
        this.Password = password;
        ForcePasswordChange = false;
        this.PublishEvent(new UserPasswordChangedEvent(this));
    }
    public void SetCompany(Guid companyId) => this.CompanyId = companyId;
    
    public static User Register(IUserUniquenessChecker userUniquenessChecker,
        IGeneratePasswordUtil generatePasswordUtil,
        int role,
        string name,
        string email,
        string password,
        string hashPassword)
    {
        return new User(userUniquenessChecker, generatePasswordUtil, role, name, email, password, hashPassword);
    }
}