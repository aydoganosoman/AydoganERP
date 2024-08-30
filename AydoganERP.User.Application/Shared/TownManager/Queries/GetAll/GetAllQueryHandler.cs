using AutoMapper;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.Base.Application.Repositories.TownRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.TownManager.Queries.GetAll;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetListResponse<Town>>
{
    private readonly ITownRepositroy _townRepositroy;
    private readonly IMapper _mapper;
    public GetAllQueryHandler(ITownRepositroy townRepositroy,
        IMapper mapper)
    {
        _townRepositroy = townRepositroy;
        _mapper = mapper;
    }

    public async Task<GetListResponse<Town>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Town> towns = await _townRepositroy
            .GetListAsync(cancellationToken: cancellationToken);

        var _mapperModel = _mapper.Map<GetListResponse<Town>>(towns);

        return _mapperModel;
    }
}
