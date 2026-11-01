using eCommerce.Core.Entities;

namespace eCommerce.Core.RespositoryContract;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);

    Task<bool> RevokeAsync(
        Guid refreshTokenId,
        DateTime revokedAtUtc,
        string? replacedByTokenHash);
}