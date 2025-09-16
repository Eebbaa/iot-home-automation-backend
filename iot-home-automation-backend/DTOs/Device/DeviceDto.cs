namespace iot_home_automation_backend.DTOs.Device
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    // For creating a device
    public class CreateDeviceDto
    {
        public int UserId { get; set; }
        public string DeviceName { get; set; }
        public string? Type { get; set; }
    }

    // For updating a device
    public class UpdateDeviceDto
    {
        public string DeviceName { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
    }
}
