using AutoMapper;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.CategoryManager.Commands.Update;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    private readonly IMapper _mapper;
    public UpdateCommandHandler(ICategoryRepository categoryRepository,
        IDomainEventUnitOfWork domainEventUnitOfWork,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _domainEventUnitOfWork = domainEventUnitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        Category updatedCategory = await _categoryRepository
            .GetAsync(x => x.Id == request.Id);

        if (updatedCategory == null)
            throw new Exception("Category Not Found!");

        updatedCategory.Update(request.Name, request.Color);

        await _categoryRepository.UpdateAsync(updatedCategory, cancellationToken);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<CategoryDto>(updatedCategory);

    }
}
