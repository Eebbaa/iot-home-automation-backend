using iot_home_automation_backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using iot_home_automation_backend.DTOs.Device;
namespace iot_home_automation_backend.Controllers.API.Version1
{
    

        [ApiController]
        [Route("api/v1/[controller]")]
        public class DeviceController : ControllerBase
        {
            private readonly ApplicationDbContext _context;
            
            public DeviceController(ApplicationDbContext context)
            {
                _context = context;
            }


            [HttpGet]
            public async Task<IActionResult> GetAllDevices()
            {
                var devices = await _context.Devices.ToListAsync();
                return Ok(devices);

            }
           
            [HttpGet("{id}")]
            public async Task<IActionResult> GetDevice(string id)
            {
                var device = await _context.Devices.FindAsync(id);
                if (device == null)
                {
                    return NotFound();

                }
                return Ok(device);

            }

            //Create Device
            [HttpPost]
            public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceDto device)
            {
                if (device == null)
                {
                    return BadRequest();
                }
                var newDevice = new Models.Device
                {
                    UserId = device.UserId,
                    DeviceName = device.DeviceName,
                    Type = device.Type,
                    Status = "Offline", // Default status
                    CreatedAt = DateTime.UtcNow
                };
                _context.Devices.Add(newDevice);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDevice), new { id = newDevice.Id }, device);
            }



        // Update Device
        // PUT : api/v1/device/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] UpdateDeviceDto device)
        {
            if (device == null || id <= 0)
            {
                return BadRequest();
            }
            var existingDevice = await _context.Devices.FindAsync(id);
            if (existingDevice == null)
            {
                return NotFound();
            }
            existingDevice.DeviceName = device.DeviceName;
            existingDevice.Type = device.Type;
            existingDevice.Status = device.Status;
            _context.Devices.Update(existingDevice);
            await _context.SaveChangesAsync();
            //return NoContent(); // Standard response for successful PUT without returning data
            return Ok(existingDevice);
        }
        // Delete Device
        // DELETE : api/v1/device/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var existingDevice = await _context.Devices.FindAsync(id);
            if (existingDevice == null)
            {
                return NotFound();
            }
            _context.Devices.Remove(existingDevice);
            await _context.SaveChangesAsync();
            return NoContent(); // Standard response for successful DELETE
        }









    }




}
