using AydoganERP.Base.Application.Repositories;
using AydoganERP.User.Domain.Entities;

namespace AydoganERP.User.Application.Common.Repositories;
public interface IUserToRoleRepository : IAsyncRepository<UserToRole, UserToRole, Guid>, IRepository<UserToRole, UserToRole, Guid> { }