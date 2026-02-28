using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Customer.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Customer.Application.CustomerManager.Queries.GetById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(IBaseDbContext baseDbContext, IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _baseDbContext
            .Customers
            .AsNoTracking()
            .Include(c => c.BankAccounts)
            .Include(c => c.Branches)
            .Include(c => c.Contacts)
            .Include(c => c.Numbers)
            .Include(c => c.Notes)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (customer == null)
        {
            throw new NotFoundException(nameof(Base.Domain.Modules.CustomerModule.Entities.Customer), request.Id);
        }

        return _mapper.Map<CustomerDto>(customer);
    }
}
