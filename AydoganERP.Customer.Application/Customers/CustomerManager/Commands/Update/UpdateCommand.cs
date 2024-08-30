using AydoganERP.Customer.Application.Common.Models;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Commands.Update;

public class UpdateCommand : IRequest<CustomerDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public Guid Segmentation { get; set; }
    public Guid Category { get; set; }
    public string Phone { get; set; }
    public string EMail { get; set; }
    public string Fax { get; set; }
    public int City { get; set; }
    public int Town { get; set; }
    public string Address { get; set; }
    public int Type { get; set; }
    public string TaxOffice { get; set; }
    public string TaxNumber { get; set; }
    public int ExchangeRateType { get; set; }
    public string EInvoice { get; set; }

    public List<IBANCustomerUpdateCommand> IBANs { get; set; } = new List<IBANCustomerUpdateCommand>();
    public List<AuthorizePersonCustomerUpdateCommand> AuthorizePeople { get; set; } = new List<AuthorizePersonCustomerUpdateCommand>();
}

public class IBANCustomerUpdateCommand
{
    public Guid Id { get; set; }
    public string Number { get; set; }
}

public class AuthorizePersonCustomerUpdateCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string EMail { get; set; }
    public string Notes { get; set; }
}
