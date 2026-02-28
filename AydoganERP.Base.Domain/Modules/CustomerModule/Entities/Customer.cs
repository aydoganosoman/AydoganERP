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
    public int PartyType { get; private set; }
    public int Status { get; private set; }

    public TaxInfo TaxInfo { get; private set; } = TaxInfo.Empty;
    public ContactInfo Contact { get; private set; } = ContactInfo.Empty;
    public Address? Address { get; private set; } = Address.Empty;
    
    public List<ProductSupplier> ProductSuppliers { get; private set; } = new();
    public List<CustomerBankAccount> BankAccounts { get; private set; } = new();
    public List<CustomerBranch> Branches { get; private set; } = new();
    public List<CustomerContact> Contacts { get; private set; } = new();
    public List<CustomerNumber> Numbers { get; private set; } = new();
    public List<CustomerNote> Notes { get; private set; } = new();
    
    public static Customer Create(
        Guid id,
        Guid companyId,
        string code,
        string customerName,
        string? name,
        string? surName,
        int type,
        int partyType,
        TaxInfo? taxInfo = null,
        ContactInfo? contact = null,
        Address? address = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (companyId == Guid.Empty) throw new ArgumentException("CompanyId cannot be empty.");
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Code is required.");
        if (string.IsNullOrWhiteSpace(customerName)) throw new ArgumentException("CustomerName is required.");
        if (type is not (CustomerTypeEnum.Customer or CustomerTypeEnum.Suplier or CustomerTypeEnum.Both))
            throw new ArgumentException("Customer type is not valid.");

        var customer = new Customer
        {
            Id = id,
            CompanyId = companyId,
            Code = code.Trim(),
            CustomerName = customerName.Trim(),
            Name = string.IsNullOrWhiteSpace(name) ? null : name.Trim(),
            SurName = string.IsNullOrWhiteSpace(surName) ? null : surName.Trim(),
            Type = type,
            PartyType = partyType,
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
    public void UpdateDetails(
        string customerName,
        string? name,
        string? surName,
        int type,
        int partyType,
        TaxInfo? taxInfo,
        ContactInfo? contact,
        Address? address)
    {
        if (string.IsNullOrWhiteSpace(customerName)) throw new ArgumentException("CustomerName is required.");
        if (type is not (CustomerTypeEnum.Customer or CustomerTypeEnum.Suplier or CustomerTypeEnum.Both))
            throw new ArgumentException("Customer type is not valid.");

        CustomerName = customerName.Trim();
        Name = string.IsNullOrWhiteSpace(name) ? null : name.Trim();
        SurName = string.IsNullOrWhiteSpace(surName) ? null : surName.Trim();
        Type = type;
        PartyType = partyType;
        TaxInfo = taxInfo ?? TaxInfo.Empty;
        Contact = contact ?? new ContactInfo(null, null);
        Address = address;
    }
    public void AddBankAccount(CustomerBankAccount bankAccount) => BankAccounts.Add(bankAccount);
    public void AddBranch(CustomerBranch branch) => Branches.Add(branch);
    public void AddContact(CustomerContact contact) => Contacts.Add(contact);
    public void AddNumber(CustomerNumber number) => Numbers.Add(number);
    public void AddNote(CustomerNote note) => Notes.Add(note);
}
