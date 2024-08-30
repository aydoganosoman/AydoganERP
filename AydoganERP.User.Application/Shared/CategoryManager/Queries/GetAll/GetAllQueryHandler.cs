using AutoMapper;
using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.CategoryManager.Queries.GetAll;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetListResponse<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    public GetAllQueryHandler(ICategoryRepository categoryRepository,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<GetListResponse<CategoryDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Category> outboxes = await _categoryRepository
            .GetListAsync(x => x.CompanyId == _currentUserService.WorkingCompanyId, cancellationToken: cancellationToken);

        var _mapperModel = _mapper.Map<GetListResponse<CategoryDto>>(outboxes);

        return _mapperModel;
    }
}
