using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // Register user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                var registeredUser = await _userService.RegisterUserAsync(user);
                return CreatedAtAction(nameof(Login), new { username = registeredUser.Username }, registeredUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Login user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var user = await _userService.ValidateLoginAsync(loginRequest.Username, loginRequest.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }

    // Helper model for login request
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
