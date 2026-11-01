using Dapper;
using eCommerce.Core.Entities;
using eCommerce.Core.RespositoryContract;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

public class RefreshTokenRepository(DapperDbContext dbContext)
    : IRefreshTokenRepository
{
    private readonly DapperDbContext _dbContext = dbContext;

    public async Task AddAsync(RefreshToken refreshToken)
    {
        const string sql = """
            INSERT INTO public."RefreshTokens"
                ("RefreshTokenId", "UserId", "TokenHash", "ExpiresAtUtc",
                 "CreatedAtUtc", "RevokedAtUtc", "ReplacedByTokenHash")
            VALUES
                (@RefreshTokenId, @UserId, @TokenHash, @ExpiresAtUtc,
                 @CreatedAtUtc, @RevokedAtUtc, @ReplacedByTokenHash)
            """;

        await _dbContext.GetDbConnection.ExecuteAsync(sql, refreshToken);
    }

    public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash)
    {
        const string sql = """
            SELECT *
            FROM public."RefreshTokens"
            WHERE "TokenHash" = @TokenHash
            """;

        return await _dbContext.GetDbConnection
            .QueryFirstOrDefaultAsync<RefreshToken>(sql, new { TokenHash = tokenHash });
    }

    public async Task<bool> RevokeAsync(
        Guid refreshTokenId,
        DateTime revokedAtUtc,
        string replacedByTokenHash)
    {
        const string sql = """
            UPDATE public."RefreshTokens"
            SET "RevokedAtUtc" = @RevokedAtUtc,
                "ReplacedByTokenHash" = @ReplacedByTokenHash
            WHERE "RefreshTokenId" = @RefreshTokenId
              AND "RevokedAtUtc" IS NULL
              AND "ExpiresAtUtc" > @RevokedAtUtc
            """;

        var rowsAffected = await _dbContext.GetDbConnection.ExecuteAsync(sql, new
        {
            RefreshTokenId = refreshTokenId,
            RevokedAtUtc = revokedAtUtc,
            ReplacedByTokenHash = replacedByTokenHash
        });

        return rowsAffected == 1;
    }
}