namespace iot_home_automation_backend.Models
{
    public class DeviceReading
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }   // FK
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public string? Unit { get; set; }

        // Navigation
        public Device Device { get; set; } = null!;
    }
}
