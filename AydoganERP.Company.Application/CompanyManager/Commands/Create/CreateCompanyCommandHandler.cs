using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Company.Application.Models;
using MediatR;

namespace AydoganERP.Company.Application.CompanyManager.Commands.Create;

public record CreateCompanyCommand(string Name) : IRequest<CompanyDto>;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    private readonly IBaseDbContext _baseDbContext;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;

    public CreateCompanyCommandHandler(
        IBaseDbContext baseDbContext,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _baseDbContext = baseDbContext;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = Base.Domain.Modules.CompanyModule.Entities.Company.Create(Guid.NewGuid(), request.Name);

        await _baseDbContext.Companies.AddAsync(company, cancellationToken);
        await _domainEventUnitOfWork.CommitAsync(null, cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }
}
