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
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Commands.Update;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICustomerSegmentationRepository _customerSegmentationRepository;
    private readonly ICityRepositroy _cityRepositroy;
    private readonly ITownRepositroy _townRepositroy;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;
    public UpdateCommandHandler(ICustomerRepository customerRepository,
        ICompanyRepository companyRepository,
        ICategoryRepository categoryRepository,
        ICustomerSegmentationRepository customerSegmentationRepository,
        ICityRepositroy cityRepositroy,
        ITownRepositroy townRepositroy,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _categoryRepository = categoryRepository;
        _cityRepositroy = cityRepositroy;
        _townRepositroy = townRepositroy;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        CustomerSegmentation currentSegmentation = await _customerSegmentationRepository
            .GetAsync(u => u.Id == request.Segmentation);

        City currentCity = await _cityRepositroy
             .GetAsync(x => x.Id == request.City);

        Town currentTown = await _townRepositroy
             .GetAsync(x => x.Id == request.Town);

        Category currentCategory = await _categoryRepository
             .GetAsync(x => x.Id == request.Category);

        Domain.Entities.Customer updateCustomer = await _customerRepository
            .GetAsync(predicate: x => x.Id == request.Id, include: x => x.Include(b => b.IBANs).Include(b => b.AuthorizePeople));

        updateCustomer.Update(request.Name,
              request.ShortName,
              currentSegmentation,
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

        #region Update Authorize Poople
        //updateCustomer.AuthorizePeople
        //    .ForEach(x =>
        //    {
        //        if (request.AuthorizePeople.Any(p => p.Id == x.Id) == false)
        //            _context.Entry(x).State = EntityState.Deleted;
        //    });

        //request.AuthorizePeople.ForEach(x =>
        //{
        //    if (updateCustomer.AuthorizePeople.Any(a => a.Id == x.Id))
        //        updateCustomer.UpdateAuthorizePerson(x.Id, x.Name, x.Phone, x.EMail, x.Notes);
        //    else
        //    {
        //        AuthorizePerson person = AuthorizePerson.Create(updateCustomer, x.Name, x.Phone, x.EMail, x.Notes);
        //        _context.Entry(person).State = EntityState.Added;
        //    }
        //});
        #endregion

        #region Update Ibans
        //updateCustomer.IBANs
        //    .ForEach(x =>
        //    {
        //        if (request.IBANs.Any(p => p.Id == x.Id) == false)
        //            _context.Entry(x).State = EntityState.Deleted;
        //    });

        //request.IBANs.ForEach(x =>
        //{
        //    if (updateCustomer.IBANs.Any(a => a.Id == x.Id))
        //        updateCustomer.UpdateIBAN(x.Id, x.Number);
        //    else
        //    {
        //        IBAN iban = IBAN.Create(updateCustomer, x.Number);
        //        _context.Entry(iban).State = EntityState.Added;
        //    }
        //});
        #endregion

        await _customerRepository.UpdateAsync(updateCustomer);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<CustomerDto>(updateCustomer);
    }
}
