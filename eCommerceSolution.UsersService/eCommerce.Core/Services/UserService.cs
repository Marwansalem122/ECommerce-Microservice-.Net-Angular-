

using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RespositoryContract;
using eCommerce.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace eCommerce.Core.Services
{
    public class UserService(IUserRepository userRepository, 
        IMapper mapper, 
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher<ApplicationUser>passwordHasher,
        IRefreshTokenRepository refreshTokenRepository
        ) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher= passwordHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository =
    refreshTokenRepository;

        public async Task<AuthenticationResponse?> Login(LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user?.PasswordHash is null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password);
            if (result == PasswordVerificationResult.Failed)
                return null;
            var rawRefreshToken = Convert.ToBase64String(
                    RandomNumberGenerator.GetBytes(64))
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');

            var tokenHash = Convert.ToHexString(
                SHA256.HashData(Encoding.UTF8.GetBytes(rawRefreshToken)));

            var now = DateTime.UtcNow;

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = user.UserId,
                TokenHash = tokenHash,
                CreatedAtUtc = now,
                ExpiresAtUtc = now.AddDays(7)
            });

           

            return _mapper.Map<AuthenticationResponse>(user) with
            {
                Success = true,
                Token = _jwtTokenGenerator.GenerateToken(user),
                RefreshToken = rawRefreshToken
            };
        }

        public async Task<AuthenticationResponse?> Register(RegisterRequest request)
        {
            ApplicationUser user = new()
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                PersonName = request.PersonName,
                Gender = request.Gender.ToString()
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var registeredUser = await _userRepository.AddUser(user);

            if (registeredUser is null)
                return null;

            return _mapper.Map<AuthenticationResponse>(registeredUser) with
            {
                Success = true,
                Token = null
            };
        }
        public async Task<AuthenticationResponse?> RefreshTokenAsync(string rawRefreshToken)
        {
            var now = DateTime.UtcNow;
            var oldHash = Convert.ToHexString(
                SHA256.HashData(Encoding.UTF8.GetBytes(rawRefreshToken)));

            var storedToken = await _refreshTokenRepository.GetByTokenHashAsync(oldHash);

            if (storedToken is null ||
                storedToken.RevokedAtUtc is not null ||
                storedToken.ExpiresAtUtc <= now)
            {
                return null;
            }

            var user = await _userRepository.GetUserByIdAsync(storedToken.UserId);
            if (user is null)
                return null;

            var newRawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');

            var newHash = Convert.ToHexString(
                SHA256.HashData(Encoding.UTF8.GetBytes(newRawToken)));

            var revoked = await _refreshTokenRepository.RevokeAsync(
                storedToken.RefreshTokenId,
                now,
                newHash);

            if (!revoked)
                return null;

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = user.UserId,
                TokenHash = newHash,
                CreatedAtUtc = now,
                ExpiresAtUtc = now.AddDays(7)
            });

            return _mapper.Map<AuthenticationResponse>(user) with
            {
                Success = true,
                Token = _jwtTokenGenerator.GenerateToken(user),
                RefreshToken = newRawToken
            };
        }
        public async Task LogoutAsync(string rawRefreshToken)
        {
            var tokenHash = Convert.ToHexString(
                SHA256.HashData(Encoding.UTF8.GetBytes(rawRefreshToken)));

            var storedToken = await _refreshTokenRepository.GetByTokenHashAsync(tokenHash);

            if (storedToken is null || storedToken.RevokedAtUtc is not null)
                return;

            var now = DateTime.UtcNow;

            if (storedToken.ExpiresAtUtc <= now)
                return;

            await _refreshTokenRepository.RevokeAsync(
                storedToken.RefreshTokenId,
                now,
                replacedByTokenHash: null);
        }
    }
}
