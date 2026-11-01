

using eCommerce.Core.Entities;
using eCommerce.Core.RespositoryContract;
using eCommerce.Infrastructure.DbContext;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace eCommerce.Infrastructure.Repositories
{
    public class UserRespository(DapperDbContext dbContext, IConfiguration configuration) : IUserRepository
    {
        private readonly DapperDbContext _dbContext = dbContext;
        private readonly IConfiguration _configure = configuration;

        public async Task<ApplicationUser?> AddUser(ApplicationUser user)
        {
            user.UserId = Guid.NewGuid();
            const string sql = """
        INSERT INTO public."Users"
            ("UserId", "Email", "Password", "PersonName", "Gender")
        VALUES
            (@UserId, @Email, @PasswordHash, @PersonName, @Gender)
        """;
            var connection = _dbContext.GetDbConnection;
            int rowsAffected = await connection.ExecuteAsync(sql, user);
            
            return rowsAffected > 0 ? user : null;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            const string sql = """
        SELECT
            "UserId",
            "Email",
            "Password" AS "PasswordHash",
            "PersonName",
            "Gender"
        FROM public."Users"
        WHERE "Email" = @Email
        """;

            var connection = _dbContext.GetDbConnection;
            return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(
                sql,
                new { Email = email });
        }
        public async Task<ApplicationUser?> GetUserByIdAsync(Guid userId)
        {
            const string sql = """
        SELECT "UserId", "Email", "PersonName", "Gender"
        FROM public."Users"
        WHERE "UserId" = @UserId
        """;

            return await _dbContext.GetDbConnection
                .QueryFirstOrDefaultAsync<ApplicationUser>(sql, new { UserId = userId });
        }
    }
}
