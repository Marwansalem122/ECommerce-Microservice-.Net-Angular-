using eCommerce.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Core.ServicesContract;  

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UsersController(IUserService usersService)
        {
            _usersService = usersService;
        }


        //GET /api/Users/{userID}
        [HttpGet("{userID}")]
        public async Task<IActionResult> GetUserByUserID(Guid userID)
        {
            if (userID == Guid.Empty)
            {
                return BadRequest("Invalid User ID");
            }

            UserDTO? response = await _usersService.GetUserDetailsAsync(userID);

            if (response == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
