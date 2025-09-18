using System.ComponentModel.DataAnnotations;

namespace iot_home_automation_backend.Models
{
    public class DeviceReading
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign key should match Device.Id type
        public Guid DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public string? Unit { get; set; }

        // Navigation
        public Device Device { get; set; } = null!;
    }
}
