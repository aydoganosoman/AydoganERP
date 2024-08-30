using AydoganERP.Customer.Application.Common.Models;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Commands.CreateOnlyName;

public class CreateOnlyNameCommand : IRequest<CustomerDto>
{
    public string Name { get; set; }
}
