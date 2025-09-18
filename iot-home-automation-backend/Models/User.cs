using Microsoft.AspNetCore.Identity;

namespace iot_home_automation_backend.Models
{
    public class User : IdentityUser<Guid>
    {
        // Since User already has an Id property from IdentityUser, we don't need to redefine it here.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string FullName { get; set; } 
        public ICollection<Device>? Devices { get; set; }




        //class IdentityUser
        //{
        //    public string Id { get; set; }              // string GUID by default
        //    public string UserName { get; set; }
        //    public string NormalizedUserName { get; set; }
        //    public string Email { get; set; }
        //    public string NormalizedEmail { get; set; }
        //    public bool EmailConfirmed { get; set; }
        //    public string PasswordHash { get; set; }
        //    public string SecurityStamp { get; set; }
        //    public string ConcurrencyStamp { get; set; }
        //    public string PhoneNumber { get; set; }
        //    public bool PhoneNumberConfirmed { get; set; }
        //    public bool TwoFactorEnabled { get; set; }
        //    public DateTimeOffset? LockoutEnd { get; set; }
        //    public bool LockoutEnabled { get; set; }
        //    public int AccessFailedCount { get; set; }
        //}

    }
}
