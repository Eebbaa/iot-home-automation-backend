namespace iot_home_automation_backend.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }     // Full name
        public string Email { get; set; }    // Email address
        public DateTime CreatedAt { get; set; } // Registration date
    }

    public class UpdateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class PatchUserDto
    {
        public string? FullName { get; set; }   // optional
        public string? Email { get; set; }      // optional
    }
}
