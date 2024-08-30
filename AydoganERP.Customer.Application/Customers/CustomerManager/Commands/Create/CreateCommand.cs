using AydoganERP.Customer.Application.Common.Models;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Commands.Create;

public class CreateCommand : IRequest<CustomerDto>
{
    public string Name { get; set; }
    public string? ShortName { get; set; }
    public Guid? Segmentation { get; set; }
    public Guid? Category { get; set; }
    public string? Phone { get; set; }
    public string? EMail { get; set; }
    public string? Fax { get; set; }
    public int? City { get; set; }
    public int? Town { get; set; }
    public string? Address { get; set; }
    public int? Type { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }
    public int? ExchangeRateType { get; set; }
    public string? EInvoice { get; set; }

    public List<IBANCustomerCreateCommand>? IBANs { get; set; } = new List<IBANCustomerCreateCommand>();
    public List<AuthorizePersonCustomerCreateCommand>? AuthorizePeople { get; set; } = new List<AuthorizePersonCustomerCreateCommand>();
}

public class IBANCustomerCreateCommand
{
    public string Number { get; set; }
}

public class AuthorizePersonCustomerCreateCommand
{
    public string Name { get; set; }
    public string? Phone { get; set; }
    public string? EMail { get; set; }
    public string? Notes { get; set; }
}
