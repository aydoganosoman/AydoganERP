using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Infrastructure.Persistence;
using AydoganERP.Base.Infrastructure.Repositories;
using AydoganERP.Company.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Infrastructure.Repositories;
public class CompanyRepository : BaseRepository<Base.Domain.Modules.CompanyModule.Entities.Company>, ICompanyRepository
{
    public CompanyRepository(DbContextOptions<ApplicationDbContext> dbContextOptions,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService) : base(dbContextOptions, currentUserService, dateTimeService)
    {

    }
}
