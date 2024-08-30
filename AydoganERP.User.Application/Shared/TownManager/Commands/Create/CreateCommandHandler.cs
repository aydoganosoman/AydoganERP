using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Application.Repositories.TownRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.TownManager.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Town>
{
    private readonly ITownRepositroy _townRepositroy;
    private readonly ICityRepositroy _cityRepositroy;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CreateCommandHandler(ITownRepositroy townRepositroy,
        ICityRepositroy cityRepositroy,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _townRepositroy = townRepositroy;
        _cityRepositroy = cityRepositroy;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task<Town> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        int _maxId = await _townRepositroy.GetMaxIdAsync();

        var _city = await _cityRepositroy.GetAsync(x => x.Id == request.CityId);

        var createdTown = Town.Create(_maxId + 1, _city, request.Name);

        await _townRepositroy.AddAsync(createdTown);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        return createdTown;
    }
}
