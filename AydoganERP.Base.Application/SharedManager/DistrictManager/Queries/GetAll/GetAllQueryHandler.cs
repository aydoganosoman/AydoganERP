using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.DistrictManager.Queries.GetAll;

public class GetAllQuery : IRequest<List<District>>
{
    public int CityId { get; set; }
}

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<District>>
{
    private readonly IMapper _mapper;
    private readonly IBaseDbContext _baseDbContext;

    public GetAllQueryHandler(IMapper mapper,
        IBaseDbContext baseDbContext)
    {
        _mapper = mapper;
        _baseDbContext = baseDbContext;
    }

    public async Task<List<District>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        List<District> queryList = await _baseDbContext.Districts.Where(x => x.CityId == request.CityId).ToListAsync();

        return queryList;
    }
}