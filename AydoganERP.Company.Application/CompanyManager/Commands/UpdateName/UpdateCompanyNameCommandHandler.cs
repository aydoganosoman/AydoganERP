using AutoMapper;
using AydoganERP.Base.Application.Common.Exceptions;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Company.Application.CompanyManager.Commands.UpdateName;

public record UpdateCompanyNameCommand(Guid Id, string Name) : IRequest<CompanyDto>;

public class UpdateCompanyNameCommandHandler : IRequestHandler<UpdateCompanyNameCommand, CompanyDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateCompanyNameCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(UpdateCompanyNameCommand request, CancellationToken cancellationToken)
    {
        var company = await _baseDbContext.Companies
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (company == null)
        {
            throw new NotFoundException(nameof(Company), request.Id);
        }

        company.Rename(request.Name);

        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }
}
