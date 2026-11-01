using eCommerce.Core.DTO;
using eCommerce.Core.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { Message = "Email and password are required." });
            }
            var response=await _userService.Login(request);
            if (response == null || !response.Success)
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if(request == null ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { Message = "Email and password are required." });
            }
            var response = await _userService.Register(request);
            if (response == null || !response.Success)
            {
                return BadRequest(new { Message = "Registration failed." });
            }
            return Ok(response);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(
    [FromBody] RefreshTokenRequest request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.RefreshToken))
                return BadRequest(new { Message = "Refresh token is required." });

            var response = await _userService.RefreshTokenAsync(request.RefreshToken);

            if (response is null)
                return Unauthorized(new { Message = "Invalid or expired refresh token." });

            return Ok(response);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
    [FromBody] RefreshTokenRequest request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.RefreshToken))
                return BadRequest(new { Message = "Refresh token is required." });

            await _userService.LogoutAsync(request.RefreshToken);

            return NoContent();
        }
    }
}
