using AutoMapper;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Common.Repositories;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Queries.GetAllByWorkingCompany;

public class GetAllByWorkingCompanyQueryHandler : IRequestHandler<GetAllByWorkingCompanyQuery, GetListResponse<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    public GetAllByWorkingCompanyQueryHandler(ICustomerRepository customerRepository,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<GetListResponse<CustomerDto>> Handle(GetAllByWorkingCompanyQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository
              .GetListAsync(x => x.Company.Id == _currentUserService.WorkingCompanyId);

        return _mapper.Map<GetListResponse<CustomerDto>>(customers);
    }
}
