using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iot_home_automation_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserIDinDeviceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceReadings_Devices_DeviceId1",
                table: "DeviceReadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_AspNetUsers_UserId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserId",
                table: "Devices");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DeviceId1",
                table: "DeviceReadings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId1",
                table: "Devices",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceReadings_Devices_DeviceId1",
                table: "DeviceReadings",
                column: "DeviceId1",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_AspNetUsers_UserId1",
                table: "Devices",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceReadings_Devices_DeviceId1",
                table: "DeviceReadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_AspNetUsers_UserId1",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserId1",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId1",
                table: "DeviceReadings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceReadings_Devices_DeviceId1",
                table: "DeviceReadings",
                column: "DeviceId1",
                principalTable: "Devices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_AspNetUsers_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
