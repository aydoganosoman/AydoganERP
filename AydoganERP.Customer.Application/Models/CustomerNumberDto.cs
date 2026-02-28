using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

namespace AydoganERP.Customer.Application.Models;

public class CustomerNumberDto :  IMapFrom<CustomerNumber>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public int NumberType { get; set; }
    public string Description { get; set; }
}