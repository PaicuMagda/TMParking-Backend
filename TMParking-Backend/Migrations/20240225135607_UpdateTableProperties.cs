using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMParking_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_Users_ParkingSpacesOwnerUserId",
                table: "ParkingSpaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_VehicleOwnerUserId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleOwnerUserId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpaces_ParkingSpacesOwnerUserId",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "VehicleOwnerUserId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ParkingSpacesOwnerUserId",
                table: "ParkingSpaces");

            migrationBuilder.AddColumn<int>(
                name: "VehicleOwnerId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpacesOwnerId",
                table: "ParkingSpaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleOwnerId",
                table: "Vehicles",
                column: "VehicleOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_ParkingSpacesOwnerId",
                table: "ParkingSpaces",
                column: "ParkingSpacesOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_Users_ParkingSpacesOwnerId",
                table: "ParkingSpaces",
                column: "ParkingSpacesOwnerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_VehicleOwnerId",
                table: "Vehicles",
                column: "VehicleOwnerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_Users_ParkingSpacesOwnerId",
                table: "ParkingSpaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_VehicleOwnerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleOwnerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpaces_ParkingSpacesOwnerId",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "VehicleOwnerId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ParkingSpacesOwnerId",
                table: "ParkingSpaces");

            migrationBuilder.AddColumn<int>(
                name: "VehicleOwnerUserId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpacesOwnerUserId",
                table: "ParkingSpaces",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleOwnerUserId",
                table: "Vehicles",
                column: "VehicleOwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_ParkingSpacesOwnerUserId",
                table: "ParkingSpaces",
                column: "ParkingSpacesOwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_Users_ParkingSpacesOwnerUserId",
                table: "ParkingSpaces",
                column: "ParkingSpacesOwnerUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_VehicleOwnerUserId",
                table: "Vehicles",
                column: "VehicleOwnerUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
