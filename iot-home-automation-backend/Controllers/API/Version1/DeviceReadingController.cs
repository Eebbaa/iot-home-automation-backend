using iot_home_automation_backend.Data;
using iot_home_automation_backend.DTOs.DeviceReading;
using iot_home_automation_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace iot_home_automation_backend.Controllers.API.Version1
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeviceReadingController: ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public DeviceReadingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddReading([FromBody] DeviceReadingDto dto)
        {
            var device = await _context.Devices.FindAsync(dto.DeviceId);
            if (device == null) 
                return NotFound(new {message="Device not found"});

            var reading = new DeviceReading
            {
                DeviceId = dto.DeviceId,
                Value = dto.Value,
                Timestamp = DateTime.UtcNow,
                Unit = dto.Unit
            };

            _context.DeviceReadings.Add(reading);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Reading saved successfully" });

        }

        [HttpGet("latest/{deviceId}")]
        public async Task<ActionResult<DeviceLatestReadingDto>> GetLatestReading(Guid deviceId)
        {
            var latest = await _context.DeviceReadings
                .Where(r => r.DeviceId == deviceId)
                .OrderByDescending(r => r.Timestamp)
                .FirstOrDefaultAsync();

            if (latest == null) 
                return NotFound();
            
            return new DeviceLatestReadingDto
            {
                DeviceId = latest.DeviceId,
                Value = latest.Value,
                Unit = latest.Unit,
                Timestamp = latest.Timestamp
            };
        
        
        }

        [HttpGet("history/{deviceId}")]

        public async Task<ActionResult<IEnumerable<DeviceReadingHistoryDto>>> GetHistory(Guid deviceId, int limit = 50)
        {
            var readings = await _context.DeviceReadings
                .Where(r => r.DeviceId == deviceId)
                .OrderByDescending(r => r.Timestamp)
                .Take(limit)
                .ToListAsync();
            return readings.Select(r => new DeviceReadingHistoryDto
            { 
                Id = r.Id,
                Value = r.Value,
                Unit = r.Unit,
                Timestamp = r.Timestamp
            }).ToList();
        }

    }
}
