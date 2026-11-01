

using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace eCommerce.Infrastructure.DbContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;

            string? connectionString = _configuration.GetConnectionString("PostgreConnection");
           _dbConnection = new NpgsqlConnection(connectionString);
        }
        public IDbConnection GetDbConnection => _dbConnection;

    }
}
 


