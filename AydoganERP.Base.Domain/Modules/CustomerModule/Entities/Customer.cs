using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.Enums;
using AydoganERP.Base.Domain.Modules.CustomerModule.Events;
using AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

public class Customer : Entity
{
    // For EF
    public Customer() { }
    
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; }
    public string Code { get; private set; } = default!;
    public string CustomerName { get; private set; } = default!;
    public string? Name { get; private set; } = default!;
    public string? SurName { get; private set; } = default!;
    public int Type { get; private set; }
    public int Status { get; private set; }

    public TaxInfo TaxInfo { get; private set; } = TaxInfo.Empty;
    public ContactInfo Contact { get; private set; } = new(null, null);
    public Address? Address { get; private set; }
    public string? Note { get; private set; }
    
    public List<Product> Products { get; private set; } = new();
    
    public static Customer Create(
        Guid id,
        string code,
        string customerName,
        string name,
        string? surName,
        int type,
        TaxInfo? taxInfo = null,
        ContactInfo? contact = null,
        Address? address = null)
    {
        // if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        // if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code is required.");
        // if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");

        var customer = new Customer
        {
            Id = id,
            Code = code.Trim(),
            CustomerName = customerName.Trim(),
            Name = name.Trim(),
            SurName =  surName.Trim(),
            Type = type,
            Status = CustomerStatusEnum.Active,
            TaxInfo = taxInfo ?? TaxInfo.Empty,
            Contact = contact ?? new ContactInfo(null, null),
            Address = address,
        };

        customer.PublishEvent(new CustomerCreatedEvent(customer.Id, customer.Code));
        
        return customer;
    }
    
    public void ChangeStatus(int newStatus)
    {
        if (Status == newStatus) return;

        // Kural: Passive’e alırken vs ek kural eklenebilir.
        var old = Status;
        Status = newStatus;

        PublishEvent(new CustomerStatusChangedEvent(Id, old, newStatus));
    }
    
}