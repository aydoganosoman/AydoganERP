using AydoganERP.Base.Application.Models;
using AydoganERP.Customer.Application.Common.Models;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Queries.GetAllByWorkingCompany;

public class GetAllByWorkingCompanyQuery : IRequest<GetListResponse<CustomerDto>>
{
}
