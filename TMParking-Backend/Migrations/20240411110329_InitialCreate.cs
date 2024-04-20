using System;
using Microsoft.EntityFrameworkCore;
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
            migrationBuilder.CreateTable(
                name: "TimisoaraAreas",
                columns: table => new
                {
                    IdTimisoaraArea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimisoaraAreas", x => x.IdTimisoaraArea);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PNC = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpaces",
                columns: table => new
                {
                    ParkingSpacesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableParkingSpaces = table.Column<int>(type: "int", nullable: false),
                    IsCargoVehicleAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsPersonalVehicleAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsPublicTransportAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsAgriculturalMachineryAccepted = table.Column<bool>(type: "bit", nullable: false),
                    ImageProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeasePermit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    IsVideoSurveilance = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    PaymentPerHour = table.Column<float>(type: "real", nullable: false),
                    PaymentPerDay = table.Column<float>(type: "real", nullable: false),
                    PaymentForSubscription = table.Column<float>(type: "real", nullable: false),
                    IsVerifiedByAdmin = table.Column<bool>(type: "bit", nullable: false),
                    SomethingIsWrong = table.Column<bool>(type: "bit", nullable: false),
                    UndergroundParkingLots = table.Column<bool>(type: "bit", nullable: false),
                    MultistoreyCarPark = table.Column<bool>(type: "bit", nullable: false),
                    PaidParking = table.Column<bool>(type: "bit", nullable: false),
                    ParkingSpacesOwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpaces", x => x.ParkingSpacesId);
                    table.ForeignKey(
                        name: "FK_ParkingSpaces_Users_ParkingSpacesOwnerId",
                        column: x => x.ParkingSpacesOwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageProfileBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    vehicleIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vehicleRegistrationCertificateBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerifiedByAdmin = table.Column<bool>(type: "bit", nullable: false),
                    SomethingIsWrong = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleOwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_VehicleOwnerId",
                        column: x => x.VehicleOwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpacesForOneParkingSpace",
                columns: table => new
                {
                    ParkingSpaceModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkingSpacesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpacesForOneParkingSpace", x => x.ParkingSpaceModelId);
                    table.ForeignKey(
                        name: "FK_ParkingSpacesForOneParkingSpace_ParkingSpaces_ParkingSpacesId",
                        column: x => x.ParkingSpacesId,
                        principalTable: "ParkingSpaces",
                        principalColumn: "ParkingSpacesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParkingSpaceModelId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_ParkingSpacesForOneParkingSpace_ParkingSpaceModelId",
                        column: x => x.ParkingSpaceModelId,
                        principalTable: "ParkingSpacesForOneParkingSpace",
                        principalColumn: "ParkingSpaceModelId",
                      onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Reservations_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_ParkingSpacesOwnerId",
                table: "ParkingSpaces",
                column: "ParkingSpacesOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpacesForOneParkingSpace_ParkingSpacesId",
                table: "ParkingSpacesForOneParkingSpace",
                column: "ParkingSpacesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ParkingSpaceModelId",
                table: "Reservations",
                column: "ParkingSpaceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_VehicleId",
                table: "Reservations",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleOwnerId",
                table: "Vehicles",
                column: "VehicleOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "TimisoaraAreas");

            migrationBuilder.DropTable(
                name: "ParkingSpacesForOneParkingSpace");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "ParkingSpaces");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
