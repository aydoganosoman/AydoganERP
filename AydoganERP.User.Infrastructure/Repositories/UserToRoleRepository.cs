using AydoganERP.Base.Application.Repositories;
using AydoganERP.User.Application.Common.Repositories;
using AydoganERP.User.Domain.Entities;
using AydoganERP.User.Infrastructure.Persistence;

namespace AydoganERP.User.Application.Repositories.InboxRepository;
public class UserToRoleRepository : EfRepositoryBase<UserToRole, UserToRole, Guid, ApplicationDbContext>, IUserToRoleRepository
{
    public UserToRoleRepository(ApplicationDbContext context)
        : base(context) { }
}
