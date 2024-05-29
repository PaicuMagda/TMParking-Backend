using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMParking_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTablesForReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bigParkingSpacesId",
                table: "ParkingSpacesForOneParkingSpace");

            migrationBuilder.AddColumn<int>(
                name: "bigParkingSpacesId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bigParkingSpacesId",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "bigParkingSpacesId",
                table: "ParkingSpacesForOneParkingSpace",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
