using AydoganERP.Customer.Application.Common.Models;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Queries.GetById;

public class GetByIdQuery : IRequest<CustomerDto>
{
    public string Id { get; set; }
}
