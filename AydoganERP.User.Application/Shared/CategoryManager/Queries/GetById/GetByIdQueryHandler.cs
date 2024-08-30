using AutoMapper;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using MediatR;

namespace AydoganERP.User.Application.Shared.CategoryManager.Queries.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public GetByIdQueryHandler(ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var _category = await _categoryRepository
            .GetAsync(x => x.Id == request.Id);

        return _mapper.Map<CategoryDto>(_category);
    }
}
