using AutoMapper;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Common.Repositories;
using AydoganERP.Customer.Domain.Entities;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Commands.CreateOnlyName;

public class CreateOnlyNameCommandHandler : IRequestHandler<CreateOnlyNameCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;
    public CreateOnlyNameCommandHandler(ICustomerRepository customerRepository,
        ICompanyRepository companyRepository,
        ICurrentUserService currentUserService,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _companyRepository = companyRepository;
        _currentUserService = currentUserService;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(CreateOnlyNameCommand request, CancellationToken cancellationToken)
    {
        Company workingCompany = await _companyRepository
            .GetAsync(u => u.Id == _currentUserService.WorkingCompanyId);

        Domain.Entities.Customer newCustomer = Domain.Entities.Customer.Create(workingCompany, request.Name);

        await _customerRepository.AddAsync(newCustomer, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        var _mapperCustomer = _mapper.Map<CustomerDto>(newCustomer);

        return _mapperCustomer;
    }
}
