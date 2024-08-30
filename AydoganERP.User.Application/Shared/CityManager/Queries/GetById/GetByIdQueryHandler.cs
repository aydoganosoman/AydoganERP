using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.CityManager.Queries.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, City>
{
    private readonly ICityRepositroy _cityRepositroy;
    public GetByIdQueryHandler(ICityRepositroy cityRepositroy)
    {
        _cityRepositroy = cityRepositroy;
    }

    public async Task<City> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var _city = await _cityRepositroy
            .GetAsync(x => x.Id == request.Id);

        return _city;
    }
}
