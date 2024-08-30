using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Application.Repositories.TownRepository;
using AydoganERP.User.Application.Common.Models;
using AydoganERP.User.Application.Common.Repositories;
using AydoganERP.User.Application.Companies.CompanyManager.Commands.Update;
using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Commands.Register;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICityRepositroy _cityRepositroy;
    private readonly ITownRepositroy _townRepositroy;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public UpdateCommandHandler(ICompanyRepository companyRepository,
        ICityRepositroy cityRepositroy,
        ITownRepositroy townRepositroy,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _companyRepository = companyRepository;
        _cityRepositroy = cityRepositroy;
        _townRepositroy = townRepositroy;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task<CompanyDto> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var _company = await _companyRepository.GetAsync(x => x.Id == request.Id);

        var _city = await _cityRepositroy.GetAsync(x => x.Id == request.CityId);
        var _town = await _townRepositroy.GetAsync(x => x.Id == request.TownId);

        _company.Update(request.Name,
            request.Logo,
            request.CommercialTitle,
            request.Sector,
            request.Address,
            _city,
            _town,
            request.Phone,
            request.Fax,
            request.TaxAdministration,
            request.TaxNumber);

        await _companyRepository.UpdateAsync(_company);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        return (CompanyDto)_company;
    }
}
