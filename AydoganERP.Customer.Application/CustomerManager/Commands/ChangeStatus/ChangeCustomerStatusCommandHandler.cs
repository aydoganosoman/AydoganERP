using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Customer.Application.CustomerManager.Commands.ChangeStatus;

public record ChangeCustomerStatusCommand(Guid Id, int Status) : IRequest;

public class ChangeCustomerStatusCommandHandler : IRequestHandler<ChangeCustomerStatusCommand>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;

    public ChangeCustomerStatusCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task Handle(ChangeCustomerStatusCommand request, CancellationToken cancellationToken)
    {
        var customer = await _baseDbContext.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (customer == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        if (request.Status is not (CustomerStatusEnum.Active or CustomerStatusEnum.Passive or CustomerStatusEnum.Blocked))
        {
            throw new ArgumentException("Customer status is not valid.");
        }

        customer.ChangeStatus(request.Status);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);
    }
}
