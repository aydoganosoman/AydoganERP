using AydoganERP.Base.Application.Common.Interfaces.Repositories;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;

namespace AydoganERP.Inventory.Application.Repositories;
public interface IInventoryRepository : IBaseRepository<Product> { }