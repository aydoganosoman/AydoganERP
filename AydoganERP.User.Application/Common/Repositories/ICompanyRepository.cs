using AydoganERP.Base.Application.Repositories;
using AydoganERP.User.Application.Common.Models;
using AydoganERP.User.Domain.Entities;

namespace AydoganERP.User.Application.Common.Repositories;
public interface ICompanyRepository : IAsyncRepository<Company, CompanyDto, Guid>, IRepository<Company, CompanyDto, Guid> { }