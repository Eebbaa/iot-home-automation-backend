
namespace iot_home_automation_backend.DTOs.Auth
{
    public class AuthResponseDto
    {
        public IEnumerable<string> Errors;

        public string Message { get; set; }
        public string Token { get; set; }
    }

    public class ForgotPasswordDto
    {
        public string Email { get; set; }

    }
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }

    }
}

