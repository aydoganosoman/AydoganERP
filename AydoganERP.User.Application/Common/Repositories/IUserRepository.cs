using AydoganERP.Base.Application.Repositories;
using AydoganERP.User.Application.Common.Models;

namespace AydoganERP.User.Application.Common.Repositories;
public interface IUserRepository : IAsyncRepository<AydoganERP.User.Domain.Entities.User, UserDto, Guid>, IRepository<AydoganERP.User.Domain.Entities.User, UserDto, Guid> { }