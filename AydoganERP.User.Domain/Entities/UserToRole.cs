using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Enums;
using AydoganERP.User.Domain.Enums;

namespace AydoganERP.User.Domain.Entities;

public class UserToRole : Entity<Guid>
{
    //For EF
    public UserToRole()
    {

    }

    private UserToRole(Company company, User user, RoleEnum role)
    {
        this.Id = Guid.NewGuid();
        this.Company = company;
        this.User = user;
        this.Role = role;
        this.IsEnable = true;
    }

    public User User { get; private set; }
    public Company Company { get; private set; }
    public RoleEnum Role { get; private set; }
    public bool IsEnable { get; private set; }

    public virtual List<CompanyUserAuthorization> UserAuthorizations { get; private set; } = new List<CompanyUserAuthorization>() { };

    public static UserToRole Create(Company company, User user, RoleEnum role)
    {
        return new UserToRole(company, user, role);
    }

    public void AddUserAuthorization(ModuleEnum module,
        AuthorizationTransactionTypeEnum authorizationTransactionType)
    {
        UserAuthorizations.Add(CompanyUserAuthorization.Create(this, module, authorizationTransactionType));
    }

}
