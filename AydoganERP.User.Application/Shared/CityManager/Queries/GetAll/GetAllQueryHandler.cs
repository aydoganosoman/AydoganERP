using AutoMapper;
using AydoganERP.Base.Application.Models;
using AydoganERP.Base.Application.Paging;
using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.CityManager.Queries.GetAll;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetListResponse<City>>
{
    private readonly ICityRepositroy _cityRepositroy;
    private readonly IMapper _mapper;
    public GetAllQueryHandler(ICityRepositroy cityRepositroy,
        IMapper mapper)
    {
        _cityRepositroy = cityRepositroy;
        _mapper = mapper;
    }

    public async Task<GetListResponse<City>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        IPaginate<City> cities = await _cityRepositroy
            .GetListAsync(cancellationToken: cancellationToken);

        var _mapperModel = _mapper.Map<GetListResponse<City>>(cities);

        return _mapperModel;

    }
}
