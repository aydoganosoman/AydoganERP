using AydoganERP.Base.Application.Models;
using AydoganERP.Customer.Api.GraphQL.Filters;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Customers.CustomerManager.Queries.GetAllBy;
using HotChocolate.Authorization;
using HotChocolate.Resolvers;
using MediatR;

namespace AydoganERP.Customer.Api.GraphQL.Queries;

public class CustomerQuery
{
    [Authorize]
    [UseFiltering(typeof(CustomerFilterType))]
    // Filter'a bakılacak
    public async Task<GetListResponse<CustomerDto>> GetCustomers([Service] IMediator mediator, Guid? companyId)
    {
        return await mediator.Send(new GetAllQuery() { CompanyId = companyId });
    }
}
