using AydoganERP.Base.Domain.Modules.SharedModule.Entities;

namespace AydoganERP.Base.Application.Common.Interfaces.Repositories.TownRepository;

public interface IDistrictRepositroy : IBaseRepository<District>
{
    Task<int> GetMaxIdAsync();
}