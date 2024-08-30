using AydoganERP.Base.Application.Models;
using AydoganERP.Customer.Application.Common.Models;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Queries.GetAllBy;

public class GetAllQuery : IRequest<GetListResponse<CustomerDto>>
{
    public Guid? CompanyId { get; set; }
}
