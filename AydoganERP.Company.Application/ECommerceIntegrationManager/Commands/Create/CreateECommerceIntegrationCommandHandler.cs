using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;

namespace AydoganERP.Company.Application.ECommerceIntegrationManager.Commands.Create;

public record CreateECommerceIntegrationCommand(
    Guid CompanyId,
    int IntegrationType,
    string StoreName,
    string Credentials,
    string? IntegrationUrl,
    string? Username) : IRequest<ECommerceIntegrationDto>;

public class CreateECommerceIntegrationCommandHandler : IRequestHandler<CreateECommerceIntegrationCommand, ECommerceIntegrationDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateECommerceIntegrationCommandHandler(
        IBaseDbContext context,
        IDomainEventUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ECommerceIntegrationDto> Handle(CreateECommerceIntegrationCommand request, CancellationToken cancellationToken)
    {
        var integrationId = Guid.NewGuid();

        var entity = ECommerceIntegration.Create(
            integrationId,
            request.CompanyId,
            request.IntegrationType,
            request.StoreName,
            request.Credentials,
            request.IntegrationUrl,
            request.Username);

        _context.ECommerceIntegrations.Add(entity);

        // Create default settings for this integration
        var defaults = IntegrationDefaults.Create(Guid.NewGuid(), integrationId);
        _context.IntegrationDefaults.Add(defaults);

        await _unitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<ECommerceIntegrationDto>(entity);
    }
}
