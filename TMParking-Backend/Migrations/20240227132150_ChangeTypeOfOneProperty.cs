using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMParking_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeOfOneProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vehicleRegistrationCertificate",
                table: "Vehicles",
                newName: "vehicleRegistrationCertificateBase64");

            migrationBuilder.AlterColumn<string>(
                name: "vehicleIdentificationNumber",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vehicleRegistrationCertificateBase64",
                table: "Vehicles",
                newName: "vehicleRegistrationCertificate");

            migrationBuilder.AlterColumn<int>(
                name: "vehicleIdentificationNumber",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
