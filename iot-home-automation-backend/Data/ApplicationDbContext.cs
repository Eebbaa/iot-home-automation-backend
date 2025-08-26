using iot_home_automation_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace iot_home_automation_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Device> Devices { get; set; } = null!;
    public DbSet<DeviceReading> DeviceReadings { get; set; } = null!;

    }

}
