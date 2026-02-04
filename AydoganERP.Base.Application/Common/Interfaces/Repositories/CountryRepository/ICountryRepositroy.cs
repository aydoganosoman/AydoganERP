using AydoganERP.Base.Domain.Modules.SharedModule.Entities;

namespace AydoganERP.Base.Application.Common.Interfaces.Repositories.CountryRepository;

public interface ICountryRepositroy : IBaseRepository<Country>
{
    Task<int> GetMaxIdAsync();
}