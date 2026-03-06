using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CustomerModule.ValuesObjects;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.CompanyManager.Commands.UpdateDetails;

public record UpdateCompanyDetailsCommand(
    Guid Id,
    string Name,
    int CompanyType,
    string? ShortName,
    string? TradeRegisterNo,
    string? TradeRegisterTitle,
    string? MersisNo,
    string? TapdkNo,
    string? HeadquartersAddress,
    int Currency,
    decimal Capital,
    DateTime? EstablishmentDate,
    // TaxInfo
    string? TaxNumber,
    string? TaxOffice,
    // ContactInfo
    string? Phone,
    string? Fax,
    string? Email,
    string? Website,
    // Address
    int? CountryId,
    int? CityId,
    int? DistrictId,
    string? AddressLine) : IRequest<CompanyDto>;

public class UpdateCompanyDetailsCommandHandler : IRequestHandler<UpdateCompanyDetailsCommand, CompanyDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateCompanyDetailsCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(UpdateCompanyDetailsCommand request, CancellationToken cancellationToken)
    {
        var company = await _baseDbContext.Companies
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (company == null)
        {
            throw new NotFoundException(nameof(Company), request.Id);
        }

        // İsim güncelle
        company.Rename(request.Name);

        // ValueObject'leri oluştur
        var taxInfo = new TaxInfo(request.TaxNumber, request.TaxOffice);
        var contact = new ContactInfo(request.Email, request.Phone, request.Fax, request.Website);
        var address = new Address(request.CountryId, request.CityId, request.DistrictId, request.AddressLine);

        // Detayları güncelle
        company.UpdateDetails(
            request.CompanyType,
            request.ShortName,
            request.TradeRegisterNo,
            request.TradeRegisterTitle,
            request.MersisNo,
            request.TapdkNo,
            request.HeadquartersAddress,
            request.Currency,
            request.Capital,
            request.EstablishmentDate,
            taxInfo,
            contact,
            address);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }
}
