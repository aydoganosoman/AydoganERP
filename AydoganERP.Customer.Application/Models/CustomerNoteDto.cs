using AydoganERP.Base.Application.Common.Mappings;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

namespace AydoganERP.Customer.Application.Models;

public class CustomerNoteDto : IMapFrom<CustomerNote>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }
}