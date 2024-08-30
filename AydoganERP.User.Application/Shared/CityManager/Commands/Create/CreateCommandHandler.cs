using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Application.Repositories.CityRepository;
using AydoganERP.Base.Domain.Entities;
using MediatR;

namespace AydoganERP.User.Application.Shared.CityManager.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, City>
{
    private readonly ICityRepositroy _cityRepositroy;
    private readonly IDomainEventUnitOfWork _domainEventUnitOfWork;
    public CreateCommandHandler(ICityRepositroy cityRepositroy,
        IDomainEventUnitOfWork domainEventUnitOfWork)
    {
        _cityRepositroy = cityRepositroy;
        _domainEventUnitOfWork = domainEventUnitOfWork;
    }

    public async Task<City> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        int _maxId = await _cityRepositroy.GetMaxIdAsync();

        var createdCity = City.Create(_maxId + 1, request.Name);

        await _cityRepositroy.AddAsync(createdCity);

        await _domainEventUnitOfWork.CommitAsync(cancellationToken);

        return createdCity;
    }
}
