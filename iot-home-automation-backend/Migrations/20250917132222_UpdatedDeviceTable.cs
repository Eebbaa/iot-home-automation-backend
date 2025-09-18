using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iot_home_automation_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDeviceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceReadings_Devices_DeviceId",
                table: "DeviceReadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_AspNetUsers_UserId1",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserId1",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_DeviceReadings_DeviceId",
                table: "DeviceReadings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DeviceId1",
                table: "DeviceReadings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReadings_DeviceId1",
                table: "DeviceReadings",
                column: "DeviceId1");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_DeviceReadings_DeviceId1",
                table: "DeviceReadings");

            migrationBuilder.DropColumn(
                name: "DeviceId1",
                table: "DeviceReadings");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Devices",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Devices",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId1",
                table: "Devices",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReadings_DeviceId",
                table: "DeviceReadings",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceReadings_Devices_DeviceId",
                table: "DeviceReadings",
                column: "DeviceId",
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
    }
}
