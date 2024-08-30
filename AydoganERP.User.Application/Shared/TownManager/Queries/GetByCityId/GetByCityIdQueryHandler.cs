using AutoMapper;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.Base.Application.Repositories.TownRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.TownManager.Queries.GetByCityId;

public class GetByCityIdQueryHandler : IRequestHandler<GetByCityIdQuery, GetListResponse<Town>>
{
    private readonly ITownRepositroy _townRepositroy;
    private readonly IMapper _mapper;
    public GetByCityIdQueryHandler(ITownRepositroy townRepositroy,
        IMapper mapper)
    {
        _townRepositroy = townRepositroy;
        _mapper = mapper;
    }

    public async Task<GetListResponse<Town>> Handle(GetByCityIdQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Town> towns = await _townRepositroy
            .GetListAsync(predicate: x => x.City.Id == request.CityId, cancellationToken: cancellationToken);

        var _mapperModel = _mapper.Map<GetListResponse<Town>>(towns);

        return _mapperModel;
    }
}
