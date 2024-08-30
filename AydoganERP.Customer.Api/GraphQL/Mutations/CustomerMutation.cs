using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Customers.CustomerManager.Commands.Create;
using AydoganERP.Customer.Application.Customers.CustomerManager.Commands.CreateOnlyName;
using AydoganERP.Customer.Application.Customers.CustomerManager.Commands.Update;
using HotChocolate.Authorization;
using MediatR;

namespace AydoganERP.Customer.Api.GraphQL.Mutations;

public class CustomerMutation
{
    [Authorize]
    public async Task<CustomerDto> CreateCustomer([Service] IMediator mediator, CreateCommand createCommand)
    {
        return await mediator.Send(createCommand);
    }

    [Authorize]
    public async Task<CustomerDto> CreateCustomerOnlyName([Service] IMediator mediator, CreateOnlyNameCommand createCommand)
    {
        return await mediator.Send(createCommand);
    }

    [Authorize]
    public async Task<CustomerDto> UpdateCustomer([Service] IMediator mediator, UpdateCommand updateCommand)
    {
        return await mediator.Send(updateCommand);
    }

}
