using AydoganERP.Base.Application.Repositories.CategoryRepository;
using AydoganERP.Base.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace AydoganERP.Base.Application.Repositories.CityRepository;
//public interface ICityRepositroy
//{
//    Task<City> AddAsync(City city);
//    Task<City> GetAsync(int id);
//    Task<IEnumerable<City>> GetAllAsync();
//    Task<int> GetMaxIdAsync();
//}

//public class CityRepositroy : ICityRepositroy
//{
//    private readonly IConfiguration _configuration;
//    public CityRepositroy(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public IDbConnection CreateConnection()
//    {
//        var connectionString = _configuration.GetConnectionString("DefaultConnection");
//        return new NpgsqlConnection(connectionString);
//    }

//    public async Task<City> GetAsync(int id)
//    {
//        using (var connection = CreateConnection())
//        {
//            var sql = "SELECT * FROM public.\"Cities\" WHERE \"Id\" = @Id";
//            return await connection.QueryFirstOrDefaultAsync<City>(sql, new { Id = id });
//        }
//    }

//    public async Task<IEnumerable<City>> GetAllAsync()
//    {
//        using (var connection = CreateConnection())
//        {
//            var sql = "SELECT * FROM public.\"Cities\"";
//            return await connection.QueryAsync<City>(sql);
//        }
//    }

//    public async Task<int> GetMaxIdAsync()
//    {
//        using (var connection = CreateConnection())
//        {
//            var sql = "SELECT MAX(\"Id\") FROM public.\"Cities\"";
//            return await connection.ExecuteScalarAsync<int>(sql);
//        }
//    }

//    public async Task<City> AddAsync(City city)
//    {
//        throw new NotImplementedException();
//    }
//}

public interface ICityRepositroy : IAsyncRepository<City, City, int>, IRepository<City, City, int>
{
    Task<int> GetMaxIdAsync();
}