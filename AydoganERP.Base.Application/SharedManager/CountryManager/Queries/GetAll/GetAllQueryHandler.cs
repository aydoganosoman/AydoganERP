using AutoMapper;
using AydoganERP.Base.Application.Common.Interfaces;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.SharedManager.CountryManager.Queries.GetAll;

public class GetAllQuery : IRequest<List<Country>>
{
}

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<Country>>
{
    private readonly IMapper _mapper;
    private readonly IBaseDbContext _baseDbContext;

    public GetAllQueryHandler(IMapper mapper,
        IBaseDbContext baseDbContext)
    {
        _mapper = mapper;
        _baseDbContext = baseDbContext;
    }

    public async Task<List<Country>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        List<Country> queryList = await _baseDbContext.Countries.ToListAsync();

        return queryList;
    }
}