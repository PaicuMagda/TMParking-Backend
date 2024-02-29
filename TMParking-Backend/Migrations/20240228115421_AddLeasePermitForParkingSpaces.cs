using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMParking_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddLeasePermitForParkingSpaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidParking",
                table: "ParkingSpaces");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ParkingSpaces",
                newName: "LeasePermit");

            migrationBuilder.AddColumn<string>(
                name: "ImageProfile",
                table: "ParkingSpaces",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "ParkingSpaces");

            migrationBuilder.RenameColumn(
                name: "LeasePermit",
                table: "ParkingSpaces",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<bool>(
                name: "PaidParking",
                table: "ParkingSpaces",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
