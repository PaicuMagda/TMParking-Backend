using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMParking_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ParkingSpaces_parkingSpacesForReservationId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_parkingSpacesForReservationId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "parkingSpacesForReservationId",
                table: "Reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "parkingSpacesForReservationId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_parkingSpacesForReservationId",
                table: "Reservations",
                column: "parkingSpacesForReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ParkingSpaces_parkingSpacesForReservationId",
                table: "Reservations",
                column: "parkingSpacesForReservationId",
                principalTable: "ParkingSpaces",
                principalColumn: "ParkingSpacesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
