using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMParking_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedParkingSpacesId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingSpaces",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpacesId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ParkingSpacesId",
                table: "Reservations",
                column: "ParkingSpacesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ParkingSpaces_ParkingSpacesId",
                table: "Reservations",
                column: "ParkingSpacesId",
                principalTable: "ParkingSpaces",
                principalColumn: "ParkingSpacesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ParkingSpaces_ParkingSpacesId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ParkingSpacesId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ParkingSpaces",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ParkingSpacesId",
                table: "Reservations");
        }
    }
}
