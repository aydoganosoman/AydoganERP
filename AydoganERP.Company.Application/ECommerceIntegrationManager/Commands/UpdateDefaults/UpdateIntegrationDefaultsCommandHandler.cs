using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.ECommerceIntegrationManager.Commands.UpdateDefaults;

public record UpdateIntegrationDefaultsCommand(
    Guid IntegrationId,
    bool ConsiderOrderStatuses,
    string? OrderStatuses,
    bool AutoCreateBarcode,
    int? InvoiceDateType,
    decimal DefaultVatRate,
    string? VatExemptionCode,
    string? ExportVatExemptionCode,
    Guid? ShippingFeeAccountId,
    Guid? InstallmentFeeAccountId,
    Guid? DefaultCustomerId,
    int? PaymentMethod,
    Guid? CargoCompanyId,
    Guid? DefaultCategoryId,
    Guid? EInvoiceSeriesId,
    Guid? EArchiveSeriesId,
    int OrderFilterDaysBefore) : IRequest<IntegrationDefaultsDto>;

public class UpdateIntegrationDefaultsCommandHandler : IRequestHandler<UpdateIntegrationDefaultsCommand, IntegrationDefaultsDto>
{
    private readonly IBaseDbContext _context;
    private readonly IDomainEventUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateIntegrationDefaultsCommandHandler(
        IBaseDbContext context,
        IDomainEventUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IntegrationDefaultsDto> Handle(UpdateIntegrationDefaultsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.IntegrationDefaults
            .FirstOrDefaultAsync(x => x.IntegrationId == request.IntegrationId, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(IntegrationDefaults), request.IntegrationId);

        entity.Update(
            request.ConsiderOrderStatuses,
            request.OrderStatuses,
            request.AutoCreateBarcode,
            request.InvoiceDateType,
            request.DefaultVatRate,
            request.VatExemptionCode,
            request.ExportVatExemptionCode,
            request.ShippingFeeAccountId,
            request.InstallmentFeeAccountId,
            request.DefaultCustomerId,
            request.PaymentMethod,
            request.CargoCompanyId,
            request.DefaultCategoryId,
            request.EInvoiceSeriesId,
            request.EArchiveSeriesId,
            request.OrderFilterDaysBefore);

        await _unitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<IntegrationDefaultsDto>(entity);
    }
}
