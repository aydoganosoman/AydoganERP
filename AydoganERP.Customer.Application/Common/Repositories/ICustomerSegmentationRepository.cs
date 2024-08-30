using AydoganERP.Base.Application.Repositories;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Domain.Entities;

namespace AydoganERP.Customer.Application.Common.Repositories;
public interface ICustomerSegmentationRepository : IAsyncRepository<CustomerSegmentation, CustomerSegmentationDto, Guid>, IRepository<CustomerSegmentation, CustomerSegmentationDto, Guid> { }