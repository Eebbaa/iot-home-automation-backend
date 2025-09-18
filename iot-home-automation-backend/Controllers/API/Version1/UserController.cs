using Azure;
using iot_home_automation_backend.Data;
using iot_home_automation_backend.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace iot_home_automation_backend.Controllers.API.Version1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        //   /api/v1/users/
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var users = await _context.Users.Select(u => new UserDto
                {
                    CreatedAt = u.CreatedAt,
                    Email = u.Email,
                    FullName = u.FullName
                })
                .ToListAsync();

                if (users == null || users.Count == 0)
                {
                    return NotFound(new { message = "No users found " });

                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while fetching users.", error = ex.Message });

            }
        }
        ///api/v1/users/{user_id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }


                var guidId = Guid.Parse(id);   // Throws if invalid
                var user = await _context.Users.Where(u => u.Id == guidId)
                    .Select(u => new UserDto
                    {
                        FullName = u.FullName,
                        Email = u.Email,
                        CreatedAt = u.CreatedAt })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound(new { message = $"User with ID {id} not found." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the user.", error = ex.Message });
            }
        }

        //PUT: /api/v1/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest(new { message = "User ID cannot be empty." });
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }
            user.FullName = dto.FullName;
            user.Email = dto.Email;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User updated successfully." });



        }

        //PATCH: /api/v1/users/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(string id, [FromBody] PatchUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest(new { message = "User ID cannot be empty." });

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }

            if (!string.IsNullOrWhiteSpace(dto.FullName))
                user.FullName = dto.FullName;
            if (!string.IsNullOrWhiteSpace(dto.Email))
                user.Email = dto.Email;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User partially updated successfully." });
        }

        //DELETE: /api/v1/users/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
                return BadRequest(new {message = "User ID cannot be empty."});

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = $"User wit ID {id} not found" });

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new {message = "User deleted successfully."});
        }



    }
}
