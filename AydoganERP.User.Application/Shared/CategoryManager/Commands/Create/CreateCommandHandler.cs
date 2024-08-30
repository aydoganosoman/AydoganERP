using AutoMapper;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.CategoryManager.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    public CreateCommandHandler(ICategoryRepository categoryRepository,
        ICurrentUserService currentUserService,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _currentUserService = currentUserService;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        Category createdCategory = Category.Create(_currentUserService.WorkingCompanyId,
            request.Name,
            request.Color);

        await _categoryRepository.AddAsync(createdCategory, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<CategoryDto>(createdCategory);
    }
}
