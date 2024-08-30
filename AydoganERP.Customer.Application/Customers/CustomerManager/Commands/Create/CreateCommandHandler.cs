using AutoMapper;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Application.Repositories.TownRepository;
using AydoganERP.Base.Domain.Entities;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Common.Repositories;
using AydoganERP.Customer.Domain.Entities;
using AydoganERP.Customer.Domain.Enums;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICustomerSegmentationRepository _customerSegmentationRepository;
    private readonly ICityRepositroy _cityRepositroy;
    private readonly ITownRepositroy _townRepositroy;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;
    public CreateCommandHandler(ICustomerRepository customerRepository,
        ICompanyRepository companyRepository,
        ICategoryRepository categoryRepository,
        ICustomerSegmentationRepository customerSegmentationRepository,
        ICityRepositroy cityRepositroy,
        ITownRepositroy townRepositroy,
        ICurrentUserService currentUserService,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _companyRepository = companyRepository;
        _categoryRepository = categoryRepository;
        _cityRepositroy = cityRepositroy;
        _townRepositroy = townRepositroy;
        _currentUserService = currentUserService;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(CreateCommand request, CancellationToken cancellationToken)
    {

        Company workingCompany = await _companyRepository
            .GetAsync(u => u.Id == _currentUserService.WorkingCompanyId);

        CustomerSegmentation segmentation = await _customerSegmentationRepository
            .GetAsync(u => u.Id == request.Segmentation);

        City currentCity = await _cityRepositroy
             .GetAsync(x => x.Id == request.City);

        Town currentTown = await _townRepositroy
             .GetAsync(x => x.Id == request.Town);

        Category currentCategory = await _categoryRepository
             .GetAsync(x => x.Id == request.Category);

        Domain.Entities.Customer newCustomer = Domain.Entities.Customer.Create(workingCompany,
              request.Name,
              request.ShortName,
              segmentation,
              currentCategory,
              request.Phone,
              request.EMail,
              request.Fax,
              currentCity,
              currentTown,
              request.Address,
              (CustomerTypeEnum)request.Type,
              request.TaxOffice,
              request.TaxNumber,
              (ExchangeRateTypeEnum)request.ExchangeRateType,
              request.EInvoice);

        request.AuthorizePeople.ForEach(x =>
        {
            newCustomer.AddAuthorizePerson(AuthorizePerson.Create(newCustomer, x.Name, x.Phone, x.EMail, x.Notes));
        });

        request.IBANs.ForEach(x =>
        {
            newCustomer.AddIBAN(IBAN.Create(newCustomer, x.Number));
        });

        await _customerRepository.AddAsync(newCustomer, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<CustomerDto>(newCustomer);
    }
}
