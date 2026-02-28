using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;

namespace AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

public class CustomerBranch : Entity
{
    // For EF
    public CustomerBranch() { }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ContactInfo Contact { get; private set; } = ContactInfo.Empty;
    public Address? Address { get; private set; } = Address.Empty;

    public static CustomerBranch Create(
        Guid id,
        Guid customerId,
        string name,
        string? email,
        string? phone,
        int? countryId,
        int? cityId,
        int? districtId,
        string? addressLine)
    {
        return new CustomerBranch
        {
            Id = id,
            CustomerId = customerId,
            Name = name,
            Contact = new ContactInfo(email, phone),
            Address = countryId == null && cityId == null && districtId == null && string.IsNullOrWhiteSpace(addressLine)
                ? null
                : new Address(countryId, cityId, districtId, addressLine)
        };
    }

    public void Update(string name, string? email, string? phone, int? countryId, int? cityId, int? districtId, string? addressLine)
    {
        Name = name;
        Contact = new ContactInfo(email, phone);
        Address = countryId == null && cityId == null && districtId == null && string.IsNullOrWhiteSpace(addressLine)
            ? null
            : new Address(countryId, cityId, districtId, addressLine);
    }
}
