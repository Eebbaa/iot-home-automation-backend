using System.ComponentModel.DataAnnotations;

namespace iot_home_automation_backend.Models
{
    public class Device
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }

        public string DeviceName { get; set; } = string.Empty;
        public string? Type { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public User User { get; set; } = null!;  
        public ICollection<DeviceReading> DeviceReadings { get; set; } = new List<DeviceReading>();
    }
}
