using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;

namespace AydoganERP.Company.Application.EInvoiceIntegrationManager.Commands.Create;

public record CreateEInvoiceIntegrationCommand(
    Guid CompanyId,
    int IntegrationType,
    string Settings) : IRequest<EInvoiceIntegrationDto>;

public class CreateEInvoiceIntegrationCommandHandler : IRequestHandler<CreateEInvoiceIntegrationCommand, EInvoiceIntegrationDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEInvoiceIntegrationCommandHandler(
        IBaseDbContext context,
        IDomainEventUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EInvoiceIntegrationDto> Handle(CreateEInvoiceIntegrationCommand request, CancellationToken cancellationToken)
    {
        var integrationId = Guid.NewGuid();

        var entity = EInvoiceIntegration.Create(
            integrationId,
            request.CompanyId,
            request.IntegrationType,
            request.Settings);

        _context.EInvoiceIntegrations.Add(entity);

        await _unitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<EInvoiceIntegrationDto>(entity);
    }
}
