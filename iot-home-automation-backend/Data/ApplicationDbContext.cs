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


            var hasher = new PasswordHasher<User>();

            // ==========================
            // 1) Seed Roles
            // ==========================
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();

            var adminRole = new IdentityRole<Guid>
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            var userRole = new IdentityRole<Guid>
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER"
            };

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(adminRole, userRole);

            // ==========================
            // 2) Seed Users
            // ==========================
            var adminId = Guid.NewGuid();
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var adminUser = new User
            {
                Id = adminId,
                FullName = "System Admin",
                UserName = "admin@iot.com",
                NormalizedUserName = "ADMIN@IOT.COM",
                Email = "admin@iot.com",
                NormalizedEmail = "ADMIN@IOT.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                CreatedAt = DateTime.UtcNow
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            var normalUser1 = new User
            {
                Id = user1Id,
                FullName = "Normal User 1",
                UserName = "user1@iot.com",
                NormalizedUserName = "USER1@IOT.COM",
                Email = "user1@iot.com",
                NormalizedEmail = "USER1@IOT.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                CreatedAt = DateTime.UtcNow
            };
            normalUser1.PasswordHash = hasher.HashPassword(normalUser1, "User1@123");

            var normalUser2 = new User
            {
                Id = user2Id,
                FullName = "Normal User 2",
                UserName = "user2@iot.com",
                NormalizedUserName = "USER2@IOT.COM",
                Email = "user2@iot.com",
                NormalizedEmail = "USER2@IOT.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                CreatedAt = DateTime.UtcNow
            };
            normalUser2.PasswordHash = hasher.HashPassword(normalUser2, "User2@123");

            modelBuilder.Entity<User>().HasData(adminUser, normalUser1, normalUser2);

            // ==========================
            // 3) Assign Users to Roles
            // ==========================
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = adminId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = user1Id,
                    RoleId = userRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = user2Id,
                    RoleId = userRoleId
                }
            );


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
