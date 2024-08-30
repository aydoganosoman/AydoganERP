using AydoganERP.Base.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace AydoganERP.Base.Application.Repositories.TownRepository;
//public interface ITownRepositroy
//{
//    Task<Town> GetAsync(int id);
//    Task<IEnumerable<Town>> GetByCityAsync(int cityId);
//    Task<IEnumerable<Town>> GetAllAsync();
//}

//public class TownRepositroy : ITownRepositroy
//{
//    private readonly IConfiguration _configuration;
//    public TownRepositroy(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public IDbConnection CreateConnection()
//    {
//        var connectionString = _configuration.GetConnectionString("DefaultConnection");
//        return new NpgsqlConnection(connectionString);
//    }

//    public async Task<Town> GetAsync(int id)
//    {
//        using (var connection = CreateConnection())
//        {
//            var sql = "SELECT * FROM public.\"Towns\" WHERE \"Id\" = @Id";
//            return await connection.QueryFirstOrDefaultAsync<Town>(sql, new { Id = id });
//        }
//    }

//    public async Task<IEnumerable<Town>> GetAllAsync()
//    {
//        using (var connection = CreateConnection())
//        {
//            var sql = "SELECT * FROM public.\"Towns\"";
//            return await connection.QueryAsync<Town>(sql);
//        }
//    }

//    public async Task<IEnumerable<Town>> GetByCityAsync(int cityId)
//    {
//        using (var connection = CreateConnection())
//        {
//            var sql = "SELECT * FROM public.\"Towns\" WHERE \"CityId\" = @cityId";
//            return await connection.QueryAsync<Town>(sql, new { CityId = cityId });
//        }
//    }
//}

public interface ITownRepositroy : IAsyncRepository<Town, Town, int>, IRepository<Town, Town, int>
{
    Task<int> GetMaxIdAsync();
}