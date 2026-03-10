using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.EInvoiceIntegrationManager.Commands.Update;

public record UpdateEInvoiceIntegrationCommand(
    Guid Id,
    string Settings,
    bool IsActive) : IRequest<EInvoiceIntegrationDto>;

public class UpdateEInvoiceIntegrationCommandHandler : IRequestHandler<UpdateEInvoiceIntegrationCommand, EInvoiceIntegrationDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEInvoiceIntegrationCommandHandler(
        IBaseDbContext context,
        IDomainEventUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EInvoiceIntegrationDto> Handle(UpdateEInvoiceIntegrationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EInvoiceIntegrations
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(EInvoiceIntegration), request.Id);

        entity.Update(
            request.Settings,
            request.IsActive);

        await _unitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<EInvoiceIntegrationDto>(entity);
    }
}
