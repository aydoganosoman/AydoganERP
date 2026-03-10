using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.ECommerceIntegrationManager.Commands.Update;

public record UpdateECommerceIntegrationCommand(
    Guid Id,
    string StoreName,
    string Credentials,
    string? IntegrationUrl,
    string? Username,
    bool IsActive) : IRequest<ECommerceIntegrationDto>;

public class UpdateECommerceIntegrationCommandHandler : IRequestHandler<UpdateECommerceIntegrationCommand, ECommerceIntegrationDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateECommerceIntegrationCommandHandler(
        IBaseDbContext context,
        IDomainEventUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ECommerceIntegrationDto> Handle(UpdateECommerceIntegrationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ECommerceIntegrations
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(ECommerceIntegration), request.Id);

        entity.Update(
            request.StoreName,
            request.Credentials,
            request.IntegrationUrl,
            request.Username,
            request.IsActive);

        await _unitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<ECommerceIntegrationDto>(entity);
    }
}
