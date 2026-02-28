using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;
using AydoganERP.Customer.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Customer.Application.CustomerManager.Commands.Create;

// Alt liste item record'ları
public record CreateBankAccountItem(
    string IBAN,
    string BankName,
    int CurrencyType = 0,
    int SortOrder = 0);

public record CreateBranchItem(
    string Name,
    string? Email,
    string? Phone,
    int? CountryId,
    int? CityId,
    int? DistrictId,
    string? AddressLine);

public record CreateContactItem(
    string Name,
    string Surname,
    string? Title,
    string? GSM,
    string? Email);

public record CreateNumberItem(
    int NumberType,
    string Description);

public record CreateNoteItem(
    DateTime Date,
    string Note);

public record CreateCustomerCommand(
    Guid CompanyId,
    string Code,
    string CustomerName,
    string? Name,
    string? SurName,
    int Type,
    int PartyType,
    string? TaxNumber,
    string? TaxOffice,
    string? Email,
    string? Phone,
    int? CountryId,
    int? CityId,
    int? DistrictId,
    string? AddressLine,
    List<CreateBankAccountItem>? BankAccounts = null,
    List<CreateBranchItem>? Branches = null,
    List<CreateContactItem>? Contacts = null,
    List<CreateNumberItem>? Numbers = null,
    List<CreateNoteItem>? Notes = null) : IRequest<CustomerDto>;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        // Kod benzersizliği kontrolü
        var existingCode = await _baseDbContext.Customers
            .AnyAsync(c => c.CompanyId == request.CompanyId && c.Code.ToLower() == request.Code.ToLower(), cancellationToken);

        if (existingCode)
        {
            throw new InvalidOperationException($"'{request.Code}' kodu zaten kullanılıyor. Lütfen farklı bir kod giriniz.");
        }

        var taxInfo = new TaxInfo(request.TaxNumber, request.TaxOffice);
        var contact = new ContactInfo(request.Email, request.Phone);
        var address = request.CountryId == null && request.CityId == null && request.DistrictId == null && string.IsNullOrWhiteSpace(request.AddressLine)
            ? null
            : new Address(request.CountryId, request.CityId, request.DistrictId, request.AddressLine);

        var customer = Base.Domain.Modules.CustomerModule.Entities.Customer.Create(
            Guid.NewGuid(),
            request.CompanyId,
            request.Code,
            request.CustomerName,
            request.Name,
            request.SurName,
            request.Type,
            request.PartyType,
            taxInfo,
            contact,
            address);

        await _baseDbContext.Customers.AddAsync(customer, cancellationToken);

        // Banka hesapları
        if (request.BankAccounts != null)
        {
            foreach (var item in request.BankAccounts)
            {
                var bankAccount = CustomerBankAccount.Create(
                    Guid.NewGuid(),
                    customer.Id,
                    item.IBAN,
                    item.BankName,
                    item.CurrencyType,
                    item.SortOrder);
                await _baseDbContext.CustomerBankAccounts.AddAsync(bankAccount, cancellationToken);
            }
        }

        // Şubeler
        if (request.Branches != null)
        {
            foreach (var item in request.Branches)
            {
                var branch = CustomerBranch.Create(
                    Guid.NewGuid(),
                    customer.Id,
                    item.Name,
                    item.Email,
                    item.Phone,
                    item.CountryId,
                    item.CityId,
                    item.DistrictId,
                    item.AddressLine);
                await _baseDbContext.CustomerBranches.AddAsync(branch, cancellationToken);
            }
        }

        // Yetkili kişiler
        if (request.Contacts != null)
        {
            foreach (var item in request.Contacts)
            {
                var contactPerson = CustomerContact.Create(
                    Guid.NewGuid(),
                    customer.Id,
                    item.Name,
                    item.Surname,
                    item.Title,
                    item.GSM,
                    item.Email);
                await _baseDbContext.CustomerContacts.AddAsync(contactPerson, cancellationToken);
            }
        }

        // Alıcı numaraları
        if (request.Numbers != null)
        {
            foreach (var item in request.Numbers)
            {
                var number = CustomerNumber.Create(
                    Guid.NewGuid(),
                    customer.Id,
                    item.NumberType,
                    item.Description);
                await _baseDbContext.CustomerNumbers.AddAsync(number, cancellationToken);
            }
        }

        // Notlar
        if (request.Notes != null)
        {
            foreach (var item in request.Notes)
            {
                var note = CustomerNote.Create(
                    Guid.NewGuid(),
                    customer.Id,
                    item.Date,
                    item.Note);
                await _baseDbContext.CustomerNotes.AddAsync(note, cancellationToken);
            }
        }

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        // Müşteriyi ilişkileriyle birlikte çek
        var createdCustomer = await _baseDbContext.Customers
            .Include(c => c.BankAccounts)
            .Include(c => c.Branches)
            .Include(c => c.Contacts)
            .Include(c => c.Numbers)
            .Include(c => c.Notes)
            .FirstOrDefaultAsync(c => c.Id == customer.Id, cancellationToken);

        return _mapper.Map<CustomerDto>(createdCustomer);
    }
}
