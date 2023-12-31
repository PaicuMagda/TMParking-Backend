using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMParking_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ConvertBase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "FileDataAsBase64",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileDataAsBase64",
                table: "Files");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "Files",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
