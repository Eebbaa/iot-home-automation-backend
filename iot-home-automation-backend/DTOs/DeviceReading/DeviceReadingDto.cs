namespace iot_home_automation_backend.DTOs.DeviceReading
{
    public class DeviceReadingDto
    {
        // this is Data from the device for POST API
        public Guid DeviceId { get; set; }
        public double Value { get; set; } 
        
        //optional (e.g "C", "%", "Open/Closed")
        public string? Unit {  get; set; }
    }

    public class DeviceReadingResponseDto
    {  
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public double Value { get; set; }
        public string? Unit { get; set; }
        public DateTime Timestamp { get; set; }

    }

    public class DeviceLatestReadingDto
    {
        public Guid DeviceId { get; set; }
        public double Value { get; set; }
        public string? Unit { get; set; }
        public DateTime Timestamp { get; set; }

    }

    public class DeviceReadingHistoryDto
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public string? Unit { get; set; }
        public DateTime Timestamp { get; set; }


    }
}
