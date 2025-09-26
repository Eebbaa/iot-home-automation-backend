using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iot_home_automation_backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4232739d-3bd7-4a31-b23a-d84c240a53a7"), null, "User", "USER" },
                    { new Guid("55ec2237-df2b-4156-b4a0-b63dfa2b8999"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { new Guid("29252e9f-9d32-4dff-9fd1-808e06496528"), 0, "d6f47eef-5559-4c99-944e-d17ddb518d0a", new DateTime(2025, 9, 18, 19, 2, 9, 214, DateTimeKind.Utc).AddTicks(8754), "user2@iot.com", true, "Normal User 2", false, null, "USER2@IOT.COM", "USER2@IOT.COM", "AQAAAAIAAYagAAAAEBAj0VKVPDvXsGSm+6lzNkxZjZyw9IwxkPfqA6bZZSdYcy0ObXw/aMZfA8FOzd64kw==", null, false, "9865908d-11c9-48be-841b-c5b592419b3f", false, null, "user2@iot.com" },
                    { new Guid("2b2ea73f-f0b9-4f15-8bde-b92affe0bb8c"), 0, "fa8abea3-39cf-4144-aa5b-deaa90dff5fb", new DateTime(2025, 9, 18, 19, 2, 9, 82, DateTimeKind.Utc).AddTicks(2522), "admin@iot.com", true, "System Admin", false, null, "ADMIN@IOT.COM", "ADMIN@IOT.COM", "AQAAAAIAAYagAAAAEH6aUiKwbcRInUXtDdheq/Og3AjTy1FZiFHp3zyPW1+0rWVtfjMTADFz9erVb/E/pQ==", null, false, "847f6e5b-20c3-405b-85d6-89939c52dbc9", false, null, "admin@iot.com" },
                    { new Guid("4ae58efb-4b64-4866-91b7-9d7b59b85226"), 0, "7d17f40f-67df-4fcf-9086-92e4214e8566", new DateTime(2025, 9, 18, 19, 2, 9, 144, DateTimeKind.Utc).AddTicks(9092), "user1@iot.com", true, "Normal User 1", false, null, "USER1@IOT.COM", "USER1@IOT.COM", "AQAAAAIAAYagAAAAEHxsqcU/4fIkeWerfIp4jBiEs+qTXxy03Pbo4eeitBw1lnCVXHDjEy+3I7nykc5FAA==", null, false, "806ec606-a9b9-48c8-a696-f82e39adce74", false, null, "user1@iot.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("4232739d-3bd7-4a31-b23a-d84c240a53a7"), new Guid("29252e9f-9d32-4dff-9fd1-808e06496528") },
                    { new Guid("55ec2237-df2b-4156-b4a0-b63dfa2b8999"), new Guid("2b2ea73f-f0b9-4f15-8bde-b92affe0bb8c") },
                    { new Guid("4232739d-3bd7-4a31-b23a-d84c240a53a7"), new Guid("4ae58efb-4b64-4866-91b7-9d7b59b85226") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("4232739d-3bd7-4a31-b23a-d84c240a53a7"), new Guid("29252e9f-9d32-4dff-9fd1-808e06496528") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("55ec2237-df2b-4156-b4a0-b63dfa2b8999"), new Guid("2b2ea73f-f0b9-4f15-8bde-b92affe0bb8c") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("4232739d-3bd7-4a31-b23a-d84c240a53a7"), new Guid("4ae58efb-4b64-4866-91b7-9d7b59b85226") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4232739d-3bd7-4a31-b23a-d84c240a53a7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("55ec2237-df2b-4156-b4a0-b63dfa2b8999"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("29252e9f-9d32-4dff-9fd1-808e06496528"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2b2ea73f-f0b9-4f15-8bde-b92affe0bb8c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4ae58efb-4b64-4866-91b7-9d7b59b85226"));
        }
    }
}
