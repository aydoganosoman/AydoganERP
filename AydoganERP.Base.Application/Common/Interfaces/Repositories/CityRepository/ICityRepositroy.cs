using AydoganERP.Base.Domain.Modules.SharedModule.Entities;

namespace AydoganERP.Base.Application.Common.Interfaces.Repositories.CityRepository;

public interface ICityRepositroy : IBaseRepository<City>
{
    Task<int> GetMaxIdAsync();
}