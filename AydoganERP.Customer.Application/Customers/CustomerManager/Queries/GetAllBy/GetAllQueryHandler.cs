using AutoMapper;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Customer.Application.Common.Models;
using AydoganERP.Customer.Application.Common.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Queries.GetAllBy;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetListResponse<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    public GetAllQueryHandler(ICustomerRepository customerRepository,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<GetListResponse<CustomerDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Customer, bool>> predicate = null;
        if (request.CompanyId.HasValue)
        {
            predicate = x => x.Company.Id == request.CompanyId;
        }

        var customers = await _customerRepository
              .GetListAsync(predicate: predicate, include: x => x.Include(b => b.Company));

        var _customers = _mapper.Map<GetListResponse<CustomerDto>>(customers);

        return _customers;
    }
}
