using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

namespace AydoganERP.Customer.Application.Models;

public class CustomerContactDto : IMapFrom<CustomerContact>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Title { get; set; }
    public string GSM { get; set; }
    public string Email { get; set; }
}