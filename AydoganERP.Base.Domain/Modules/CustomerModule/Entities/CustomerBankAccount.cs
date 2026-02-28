using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.CustomerModule.Entities;

public class CustomerBankAccount : Entity
{
    //For EF
    public CustomerBankAccount() { }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public int SortOrder { get; private set; }
    public string IBAN { get; private set; }
    public int CurrencyType { get; private set; }
    public string BankName { get; private set; }

    public static CustomerBankAccount Create(
        Guid id,
        Guid customerId,
        string iban,
        string bankName,
        int currencyType = 0,
        int sortOrder = 0)
    {
        return new CustomerBankAccount
        {
            Id = id,
            CustomerId = customerId,
            IBAN = iban,
            BankName = bankName,
            CurrencyType = currencyType,
            SortOrder = sortOrder
        };
    }

    public void Update(string iban, string bankName, int currencyType, int sortOrder)
    {
        IBAN = iban;
        BankName = bankName;
        CurrencyType = currencyType;
        SortOrder = sortOrder;
    }
}
