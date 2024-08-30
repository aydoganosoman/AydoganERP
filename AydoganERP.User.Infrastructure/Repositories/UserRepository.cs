using AydoganERP.Base.Application.Repositories;
using AydoganERP.User.Application.Common.Models;
using AydoganERP.User.Application.Common.Repositories;
using AydoganERP.User.Infrastructure.Persistence;

namespace AydoganERP.User.Application.Repositories.InboxRepository;
public class UserRepository : EfRepositoryBase<AydoganERP.User.Domain.Entities.User, UserDto, Guid, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext context)
        : base(context) { }
}
