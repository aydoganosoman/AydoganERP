using AydoganERP.Base.Application.Repositories;
using AydoganERP.User.Application.Common.Models;
using AydoganERP.User.Application.Common.Repositories;
using AydoganERP.User.Domain.Entities;
using AydoganERP.User.Infrastructure.Persistence;

namespace AydoganERP.User.Application.Repositories.InboxRepository;
public class CompanyRepository : EfRepositoryBase<Company, CompanyDto, Guid, ApplicationDbContext>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context)
        : base(context) { }
}
