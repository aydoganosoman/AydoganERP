using AydoganERP.Base.Application.Repositories;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Common.Repositories;
using AydoganERP.Customer.Domain.Entities;
using AydoganERP.Customer.Infrastructure.Persistence;

namespace AydoganERP.Customer.Infrastructure.Repositories;
public class CustomerSegmentationRepository : EfRepositoryBase<CustomerSegmentation, CustomerSegmentationDto, Guid, ApplicationDbContext>, ICustomerSegmentationRepository
{
    public CustomerSegmentationRepository(ApplicationDbContext context)
        : base(context) { }
}
