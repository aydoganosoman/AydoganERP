using AydoganERP.Base.Application.Repositories;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Common.Repositories;
using AydoganERP.Customer.Domain.Entities;
using AydoganERP.Customer.Infrastructure.Persistence;

namespace AydoganERP.Customer.Application.Repositories.InboxRepository;
public class CompanyRepository : EfRepositoryBase<Company, CompanyDto, Guid, ApplicationDbContext>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context)
        : base(context) { }
}
