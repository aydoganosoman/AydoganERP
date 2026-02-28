using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Customer.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Customer.Application.CustomerManager.Queries.GetList;

public record GetCustomerListQuery(Guid? CompanyId = null) : IRequest<List<CustomerDto>>;

public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, List<CustomerDto>>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetCustomerListQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<List<CustomerDto>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
    {
        var query = _baseDbContext.Customers.AsNoTracking();

        if (request.CompanyId != null)
            query = query.Where(x => x.CompanyId == request.CompanyId);

        var customers = await query.ToListAsync(cancellationToken: cancellationToken);

        return _mapper.Map<List<CustomerDto>>(customers);
    }
}
