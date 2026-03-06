using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.CityManager.Queries.GetAll;

public class GetAllQuery : IRequest<List<City>>
{
    public int CountryId { get; set; }
}

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<City>>
{
    private readonly IMapper _mapper;
    private readonly IBaseDbContext _baseDbContext;

    public GetAllQueryHandler(IMapper mapper,
        IBaseDbContext baseDbContext)
    {
        _mapper = mapper;
        _baseDbContext = baseDbContext;
    }

    public async Task<List<City>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        List<City> queryList = await _baseDbContext.Cities.Where(x => x.CountryId == request.CountryId).ToListAsync();

        return queryList;
    }
}