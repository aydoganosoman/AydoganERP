using AydoganERP.Base.Application.Repositories;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Common.Repositories;
using AydoganERP.Customer.Infrastructure.Persistence;

namespace AydoganERP.Customer.Infrastructure.Repositories;
public class CustomerRepository : EfRepositoryBase<Domain.Entities.Customer, CustomerDto, Guid, ApplicationDbContext>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context)
        : base(context) { }
}
