using AutoMapper;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Common.Repositories;
using MediatR;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Queries.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    public GetByIdQueryHandler(ICustomerRepository customerRepository,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsync(x => x.Id == new Guid(request.Id));

        return _mapper.Map<CustomerDto>(customer);
    }
}
