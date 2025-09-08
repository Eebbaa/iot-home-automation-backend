using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore. Identity;
using iot_home_automation_backend.Models;
using iot_home_automation_backend.DTOs.Auth;
namespace iot_home_automation_backend.Controllers.API.Version1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // POST: api/v1/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                // Optionally, you can sign in the user immediately after registration
                // await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new AuthResponseDto{ Message = "User registered successfully" });
                
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }

        //Post: api/v1/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok(new AuthResponseDto { Message = "User logged in successfully" });
              
            }
            else
            {
               return Unauthorized(new AuthResponseDto { Message = "Invalid login attempt" });
               
            }
        }

        //POST: api/v1/auth/logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok(new AuthResponseDto { Message = $"User logged out successfully" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new AuthResponseDto { Message=$"Logout failed: {ex.Message}"});

            }
        }

        
        

        

    }
}
