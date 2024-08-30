using AydoganERP.Base.Application.Repositories.TownRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.TownManager.Queries.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Town>
{
    private readonly ITownRepositroy _townRepositroy;
    public GetByIdQueryHandler(ITownRepositroy townRepositroy)
    {
        _townRepositroy = townRepositroy;
    }

    public async Task<Town> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var _town = await _townRepositroy
            .GetAsync(x => x.Id == request.Id);

        return _town;
    }
}
