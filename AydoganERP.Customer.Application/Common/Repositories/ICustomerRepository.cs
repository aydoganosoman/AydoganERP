using AydoganERP.Base.Application.Repositories;
using AydoganERP.Customer.Application.Common.Models;

namespace AydoganERP.Customer.Application.Common.Repositories;
public interface ICustomerRepository : IAsyncRepository<Domain.Entities.Customer, CustomerDto, Guid>, IRepository<Domain.Entities.Customer, CustomerDto, Guid> { }