using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Enums;

namespace AydoganERP.User.Domain.Entities;

public class CompanyUserAuthorization : Entity<Guid>
{
    public CompanyUserAuthorization()
    {

    }

    private CompanyUserAuthorization(UserToRole userRole,
        ModuleEnum module,
        AuthorizationTransactionTypeEnum authorizationTransactionType)
    {
        this.Id = Guid.NewGuid();
        this.UserRole = userRole;
        this.AuthorizationTransactionType = authorizationTransactionType;
        this.Module = module;
    }

    public UserToRole UserRole { get; private set; }
    public ModuleEnum Module { get; private set; }
    public AuthorizationTransactionTypeEnum AuthorizationTransactionType { get; private set; }

    public static CompanyUserAuthorization Create(UserToRole userRole,
        ModuleEnum module,
        AuthorizationTransactionTypeEnum authorizationTransactionType)
    {
        return new CompanyUserAuthorization(userRole, module, authorizationTransactionType);
    }
}
