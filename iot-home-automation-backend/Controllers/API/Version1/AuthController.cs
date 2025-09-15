using iot_home_automation_backend.DTOs.Auth;
using iot_home_automation_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore. Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace iot_home_automation_backend.Controllers.API.Version1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private UserManager<User> object1;
        private SignInManager<User> object2;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        public AuthController(UserManager<User> object1, SignInManager<User> object2)
        {
            this.object1 = object1;
            this.object2 = object2;
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
            if (!result.Succeeded)
            {

                //return Ok(new AuthResponseDto { Message = "User logged in successfully" });
                return Unauthorized(new  AuthResponseDto{ Message = "Invalid login attempt" });
            }
            //else
            //{
            //   return Unauthorized(new AuthResponseDto { Message = "Invalid login attempt" });

            //}
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            var token = await GenerateJwtToken(user);

            return Ok(new AuthResponseDto
            {
                Message = "User logged in successfully",
                Token = token
            });
        }

        //POST: api/v1/auth/logout
        [HttpPost("logout")]
        [Authorize]
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


        // password recovery needs
        // 1. forget password-->
        // 2.send token to email--->
        // 3. Reset password by using valid token

        //POST: api/v1/auth/forgot-password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound(new AuthResponseDto { Message = "User with this email does not exist."});
            
            //generate password resety token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Ok(new AuthResponseDto { Message = "Password reset token generated successfully.", Token = token });
        
        }

        // POST: api/v1/auth/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return NotFound(new AuthResponseDto { Message ="User not found." });

            var result = await _userManager.ResetPasswordAsync(user, dto.Token,dto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new AuthResponseDto { Message = "Password reset failed.", Errors = result.Errors.Select(e =>e.Description)});
            }

            return Ok(new AuthResponseDto { Message = "Password reset successfully." });

        }


        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {   
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName ?? user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            
            var creds = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                         Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])
                ),
                 
                 signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        


    }
}
