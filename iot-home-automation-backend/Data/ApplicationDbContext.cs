using iot_home_automation_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iot_home_automation_backend.Data
{
    public class ApplicationDbContext:IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
        //Identity will manage to create the user Table no need to mention it here.
        // public DbSet<User> Users { get; set; } = null!;
        public DbSet<Device> Devices { get; set; } = null!;
        public DbSet<DeviceReading> DeviceReadings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Device.Id as auto-generated GUID
            modelBuilder.Entity<Device>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<DeviceReading>()
                .HasOne(dr => dr.Device)
                .WithMany(d => d.DeviceReadings)
                .HasForeignKey(dr => dr.DeviceId);
        }

    }


}
