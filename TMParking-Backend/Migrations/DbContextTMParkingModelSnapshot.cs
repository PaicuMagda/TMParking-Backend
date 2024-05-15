﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TMParking_Backend.Data;

#nullable disable

namespace TMParking_Backend.Migrations
{
    [DbContext(typeof(DbContextTMParking))]
    partial class DbContextTMParkingModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TMParking_Backend.Models.ParkingSpaceModel", b =>
                {
                    b.Property<int>("ParkingSpaceModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParkingSpaceModelId"));

                    b.Property<string>("Availability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParkingSpacesId")
                        .HasColumnType("int");

                    b.HasKey("ParkingSpaceModelId");

                    b.HasIndex("ParkingSpacesId");

                    b.ToTable("ParkingSpacesForOneParkingSpace");
                });

            modelBuilder.Entity("TMParking_Backend.Models.ParkingSpaces", b =>
                {
                    b.Property<int>("ParkingSpacesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParkingSpacesId"));

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AvailableParkingSpaces")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageProfile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAgriculturalMachineryAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCargoVehicleAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDraft")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPersonalVehicleAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublicTransportAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerifiedByAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVideoSurveilance")
                        .HasColumnType("bit");

                    b.Property<string>("LeasePermit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MultistoreyCarPark")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PaidParking")
                        .HasColumnType("bit");

                    b.Property<int>("ParkingSpacesOwnerId")
                        .HasColumnType("int");

                    b.Property<float>("PaymentForSubscription")
                        .HasColumnType("real");

                    b.Property<float>("PaymentPerDay")
                        .HasColumnType("real");

                    b.Property<float>("PaymentPerHour")
                        .HasColumnType("real");

                    b.Property<bool>("SomethingIsWrong")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("UndergroundParkingLots")
                        .HasColumnType("bit");

                    b.HasKey("ParkingSpacesId");

                    b.HasIndex("ParkingSpacesOwnerId");

                    b.ToTable("ParkingSpaces");
                });

            modelBuilder.Entity("TMParking_Backend.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfMonths")
                        .HasColumnType("int");

                    b.Property<int>("ParkingSpaceModelId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PriceToPay")
                        .HasColumnType("real");

                    b.Property<string>("ReservationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpaceModelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleRegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReservationId");

                    b.HasIndex("ParkingSpaceModelId");

                    b.HasIndex("UserId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("TMParking_Backend.Models.TimisoaraArea", b =>
                {
                    b.Property<int>("IdTimisoaraArea")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTimisoaraArea"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTimisoaraArea");

                    b.ToTable("TimisoaraAreas");
                });

            modelBuilder.Entity("TMParking_Backend.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PNC")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ResetPasswordExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TMParking_Backend.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageProfileBase64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerifiedByAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Make")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SomethingIsWrong")
                        .HasColumnType("bit");

                    b.Property<int>("VehicleOwnerId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<string>("vehicleIdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vehicleRegistrationCertificateBase64")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleId");

                    b.HasIndex("VehicleOwnerId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("TMParking_Backend.Models.ParkingSpaceModel", b =>
                {
                    b.HasOne("TMParking_Backend.Models.ParkingSpaces", "ParkingSpaces")
                        .WithMany("ParkingSpaceForOneParking")
                        .HasForeignKey("ParkingSpacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingSpaces");
                });

            modelBuilder.Entity("TMParking_Backend.Models.ParkingSpaces", b =>
                {
                    b.HasOne("TMParking_Backend.Models.User", "ParkingSpacesOwner")
                        .WithMany("ParkingSpaces")
                        .HasForeignKey("ParkingSpacesOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingSpacesOwner");
                });

            modelBuilder.Entity("TMParking_Backend.Models.Reservation", b =>
                {
                    b.HasOne("TMParking_Backend.Models.ParkingSpaceModel", "ParkingSpaceModel")
                        .WithMany("Reservations")
                        .HasForeignKey("ParkingSpaceModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TMParking_Backend.Models.User", null)
                        .WithMany("Reservations")
                        .HasForeignKey("UserId");

                    b.HasOne("TMParking_Backend.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingSpaceModel");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TMParking_Backend.Models.Vehicle", b =>
                {
                    b.HasOne("TMParking_Backend.Models.User", "VehicleOwner")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleOwner");
                });

            modelBuilder.Entity("TMParking_Backend.Models.ParkingSpaceModel", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("TMParking_Backend.Models.ParkingSpaces", b =>
                {
                    b.Navigation("ParkingSpaceForOneParking");
                });

            modelBuilder.Entity("TMParking_Backend.Models.User", b =>
                {
                    b.Navigation("ParkingSpaces");

                    b.Navigation("Reservations");

                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
