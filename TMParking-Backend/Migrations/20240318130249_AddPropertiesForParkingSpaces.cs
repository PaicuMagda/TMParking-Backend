using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMParking_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesForParkingSpaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MultistoreyCarPark",
                table: "ParkingSpaces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PaidParking",
                table: "ParkingSpaces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UndergroundParkingLots",
                table: "ParkingSpaces",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultistoreyCarPark",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "PaidParking",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "UndergroundParkingLots",
                table: "ParkingSpaces");
        }
    }
}
