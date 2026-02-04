using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Base.Infrastructure.Persistence;
using AydoganERP.Base.Infrastructure.Repositories;
using AydoganERP.Identity.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Identity.Infrastructure.Repositories;
public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DbContextOptions<ApplicationDbContext> dbContextOptions,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService) : base(dbContextOptions, currentUserService, dateTimeService)
    {

    }
}
