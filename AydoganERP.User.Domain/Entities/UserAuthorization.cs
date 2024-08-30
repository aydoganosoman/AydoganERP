using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Enums;

namespace AydoganERP.User.Domain.Entities;

public class UserAuthorization : Entity<Guid>
{
    public UserAuthorization()
    {

    }

    private UserAuthorization(User user,
        AuthorizationTransactionTypeEnum authorizationTransactionType,
        PermissionEnum permission)
    {
        this.Id = Guid.NewGuid();
        this.User = user;
        this.AuthorizationTransactionType = authorizationTransactionType;
        this.Permission = permission;
    }

    public User User { get; private set; }
    public AuthorizationTransactionTypeEnum AuthorizationTransactionType { get; private set; }
    public PermissionEnum Permission { get; private set; }

    public static UserAuthorization Create (User user,
        AuthorizationTransactionTypeEnum authorizationTransactionType,
        PermissionEnum permission)
    {
        return new UserAuthorization(user, authorizationTransactionType, permission);
    }
}
