using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;
using AydoganERP.Customer.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Customer.Application.CustomerManager.Commands.Update;

// Alt liste item record'ları
public record UpdateBankAccountItem(
    Guid? Id,
    string IBAN,
    string BankName,
    int CurrencyType = 0,
    int SortOrder = 0);

public record UpdateBranchItem(
    Guid? Id,
    string Name,
    string? Email,
    string? Phone,
    int? CountryId,
    int? CityId,
    int? DistrictId,
    string? AddressLine);

public record UpdateContactItem(
    Guid? Id,
    string Name,
    string Surname,
    string? Title,
    string? GSM,
    string? Email);

public record UpdateNumberItem(
    Guid? Id,
    int NumberType,
    string Description);

public record UpdateNoteItem(
    Guid? Id,
    DateTime Date,
    string Note);

public record UpdateCustomerCommand(
    Guid Id,
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
    List<UpdateBankAccountItem>? BankAccounts = null,
    List<UpdateBranchItem>? Branches = null,
    List<UpdateContactItem>? Contacts = null,
    List<UpdateNumberItem>? Numbers = null,
    List<UpdateNoteItem>? Notes = null) : IRequest<CustomerDto>;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _baseDbContext.Customers
            .Include(c => c.BankAccounts)
            .Include(c => c.Branches)
            .Include(c => c.Contacts)
            .Include(c => c.Numbers)
            .Include(c => c.Notes)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.Id);

        var taxInfo = new TaxInfo(request.TaxNumber, request.TaxOffice);
        var contact = new ContactInfo(request.Email, request.Phone);
        var address = request.CountryId == null && request.CityId == null && request.DistrictId == null && string.IsNullOrWhiteSpace(request.AddressLine)
            ? null
            : new Address(request.CountryId, request.CityId, request.DistrictId, request.AddressLine);

        customer.UpdateDetails(
            request.CustomerName,
            request.Name,
            request.SurName,
            request.Type,
            request.PartyType,
            taxInfo,
            contact,
            address);

        // Alt listeleri güncelle
        await UpdateBankAccountsAsync(customer, request.BankAccounts, cancellationToken);
        await UpdateBranchesAsync(customer, request.Branches, cancellationToken);
        await UpdateContactsAsync(customer, request.Contacts, cancellationToken);
        await UpdateNumbersAsync(customer, request.Numbers, cancellationToken);
        await UpdateNotesAsync(customer, request.Notes, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        // Güncellenmiş müşteriyi çek
        var updatedCustomer = await _baseDbContext.Customers
            .Include(c => c.BankAccounts)
            .Include(c => c.Branches)
            .Include(c => c.Contacts)
            .Include(c => c.Numbers)
            .Include(c => c.Notes)
            .FirstOrDefaultAsync(c => c.Id == customer.Id, cancellationToken);

        return _mapper.Map<CustomerDto>(updatedCustomer);
    }

    private async Task UpdateBankAccountsAsync(
        Base.Domain.Modules.CustomerModule.Entities.Customer customer,
        List<UpdateBankAccountItem>? requestItems,
        CancellationToken cancellationToken)
    {
        if (requestItems == null) return;

        var existingItems = customer.BankAccounts.ToList();
        var requestIds = requestItems.Where(x => x.Id.HasValue && x.Id != Guid.Empty).Select(x => x.Id!.Value).ToHashSet();

        // Sil
        foreach (var item in existingItems.Where(e => !requestIds.Contains(e.Id)))
            _baseDbContext.CustomerBankAccounts.Remove(item);

        // Güncelle veya Ekle
        foreach (var item in requestItems)
        {
            if (item.Id.HasValue && item.Id != Guid.Empty)
            {
                var existing = existingItems.FirstOrDefault(e => e.Id == item.Id);
                existing?.Update(item.IBAN, item.BankName, item.CurrencyType, item.SortOrder);
            }
            else
            {
                var newItem = CustomerBankAccount.Create(Guid.NewGuid(), customer.Id, item.IBAN, item.BankName, item.CurrencyType, item.SortOrder);
                await _baseDbContext.CustomerBankAccounts.AddAsync(newItem, cancellationToken);
            }
        }
    }

    private async Task UpdateBranchesAsync(
        Base.Domain.Modules.CustomerModule.Entities.Customer customer,
        List<UpdateBranchItem>? requestItems,
        CancellationToken cancellationToken)
    {
        if (requestItems == null) return;

        var existingItems = customer.Branches.ToList();
        var requestIds = requestItems.Where(x => x.Id.HasValue && x.Id != Guid.Empty).Select(x => x.Id!.Value).ToHashSet();

        foreach (var item in existingItems.Where(e => !requestIds.Contains(e.Id)))
            _baseDbContext.CustomerBranches.Remove(item);

        foreach (var item in requestItems)
        {
            if (item.Id.HasValue && item.Id != Guid.Empty)
            {
                var existing = existingItems.FirstOrDefault(e => e.Id == item.Id);
                existing?.Update(item.Name, item.Email, item.Phone, item.CountryId, item.CityId, item.DistrictId, item.AddressLine);
            }
            else
            {
                var newItem = CustomerBranch.Create(Guid.NewGuid(), customer.Id, item.Name, item.Email, item.Phone, item.CountryId, item.CityId, item.DistrictId, item.AddressLine);
                await _baseDbContext.CustomerBranches.AddAsync(newItem, cancellationToken);
            }
        }
    }

    private async Task UpdateContactsAsync(
        Base.Domain.Modules.CustomerModule.Entities.Customer customer,
        List<UpdateContactItem>? requestItems,
        CancellationToken cancellationToken)
    {
        if (requestItems == null) return;

        var existingItems = customer.Contacts.ToList();
        var requestIds = requestItems.Where(x => x.Id.HasValue && x.Id != Guid.Empty).Select(x => x.Id!.Value).ToHashSet();

        foreach (var item in existingItems.Where(e => !requestIds.Contains(e.Id)))
            _baseDbContext.CustomerContacts.Remove(item);

        foreach (var item in requestItems)
        {
            if (item.Id.HasValue && item.Id != Guid.Empty)
            {
                var existing = existingItems.FirstOrDefault(e => e.Id == item.Id);
                existing?.Update(item.Name, item.Surname, item.Title, item.GSM, item.Email);
            }
            else
            {
                var newItem = CustomerContact.Create(Guid.NewGuid(), customer.Id, item.Name, item.Surname, item.Title, item.GSM, item.Email);
                await _baseDbContext.CustomerContacts.AddAsync(newItem, cancellationToken);
            }
        }
    }

    private async Task UpdateNumbersAsync(
        Base.Domain.Modules.CustomerModule.Entities.Customer customer,
        List<UpdateNumberItem>? requestItems,
        CancellationToken cancellationToken)
    {
        if (requestItems == null) return;

        var existingItems = customer.Numbers.ToList();
        var requestIds = requestItems.Where(x => x.Id.HasValue && x.Id != Guid.Empty).Select(x => x.Id!.Value).ToHashSet();

        foreach (var item in existingItems.Where(e => !requestIds.Contains(e.Id)))
            _baseDbContext.CustomerNumbers.Remove(item);

        foreach (var item in requestItems)
        {
            if (item.Id.HasValue && item.Id != Guid.Empty)
            {
                var existing = existingItems.FirstOrDefault(e => e.Id == item.Id);
                existing?.Update(item.NumberType, item.Description);
            }
            else
            {
                var newItem = CustomerNumber.Create(Guid.NewGuid(), customer.Id, item.NumberType, item.Description);
                await _baseDbContext.CustomerNumbers.AddAsync(newItem, cancellationToken);
            }
        }
    }

    private async Task UpdateNotesAsync(
        Base.Domain.Modules.CustomerModule.Entities.Customer customer,
        List<UpdateNoteItem>? requestItems,
        CancellationToken cancellationToken)
    {
        if (requestItems == null) return;

        var existingItems = customer.Notes.ToList();
        var requestIds = requestItems.Where(x => x.Id.HasValue && x.Id != Guid.Empty).Select(x => x.Id!.Value).ToHashSet();

        foreach (var item in existingItems.Where(e => !requestIds.Contains(e.Id)))
            _baseDbContext.CustomerNotes.Remove(item);

        foreach (var item in requestItems)
        {
            if (item.Id.HasValue && item.Id != Guid.Empty)
            {
                var existing = existingItems.FirstOrDefault(e => e.Id == item.Id);
                existing?.Update(item.Date, item.Note);
            }
            else
            {
                var newItem = CustomerNote.Create(Guid.NewGuid(), customer.Id, item.Date, item.Note);
                await _baseDbContext.CustomerNotes.AddAsync(newItem, cancellationToken);
            }
        }
    }
}
