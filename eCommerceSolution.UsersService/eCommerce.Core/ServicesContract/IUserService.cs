using eCommerce.Core.DTO;

namespace eCommerce.Core.ServicesContract
{
    public interface IUserService
    {
        Task<AuthenticationResponse?> Register(RegisterRequest request);
        Task<AuthenticationResponse?> Login(LoginRequest request);
        Task<AuthenticationResponse?> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
    }
}
