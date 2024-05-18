using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMParking_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ParkingSpaces_ParkingSpacesId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ParkingSpacesId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ParkingSpacesId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ParkingSpaces",
                table: "Reservations",
                newName: "parkingSpacesForReservationId");

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
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ParkingSpaces_parkingSpacesForReservationId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_parkingSpacesForReservationId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "parkingSpacesForReservationId",
                table: "Reservations",
                newName: "ParkingSpaces");

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
    }
}
