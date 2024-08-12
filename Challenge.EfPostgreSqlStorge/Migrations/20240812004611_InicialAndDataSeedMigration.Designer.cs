﻿// <auto-generated />
using System;
using Challenge.EfPostgreSqlStorge;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Challenge.EfPostgreSqlStorge.Migrations
{
    [DbContext(typeof(EfPgsqlContext))]
    [Migration("20240812004611_InicialAndDataSeedMigration")]
    partial class InicialAndDataSeedMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Challenge.Domain.Entities.Cities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AirportCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("AirportName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Cities", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("975ed3c2-16a7-48f8-bb26-a485d00c6b5c"),
                            AirportCode = "SDQ",
                            AirportName = "Aeropuerto Internacional de Las Américas",
                            CityName = "Santo Domingo"
                        },
                        new
                        {
                            Id = new Guid("0fc9af32-a83b-49d0-96a3-0a129959a765"),
                            AirportCode = "PUJ",
                            AirportName = "Aeropuerto Internacional de Punta Cana",
                            CityName = "Punta Cana"
                        },
                        new
                        {
                            Id = new Guid("e2e25b32-f813-46b9-9789-f1808589e98a"),
                            AirportCode = "MCO",
                            AirportName = "Aeropuerto Internacional de Orlando",
                            CityName = "Orlando"
                        },
                        new
                        {
                            Id = new Guid("3a8c1e28-f627-4ca5-872c-1fb922bdbd88"),
                            AirportCode = "MIA",
                            AirportName = "Aeropuerto Internacional de Miami",
                            CityName = "Miami"
                        },
                        new
                        {
                            Id = new Guid("6b487064-b471-47ce-87c2-257118658842"),
                            AirportCode = "JFK",
                            AirportName = "Aeropuerto Internacional John F. Kennedy",
                            CityName = "Nueva York"
                        },
                        new
                        {
                            Id = new Guid("369d93a7-ebdc-4f14-8a70-4f28164303c3"),
                            AirportCode = "FLL",
                            AirportName = "Aeropuerto Internacional de Fort Lauderdale-Hollywood",
                            CityName = "Fort Lauderdale"
                        },
                        new
                        {
                            Id = new Guid("c2f4a542-df4b-4997-be91-7140827bca80"),
                            AirportCode = "MDE",
                            AirportName = "Aeropuerto Internacional José María Córdova",
                            CityName = "Medellín"
                        });
                });

            modelBuilder.Entity("Challenge.Domain.Entities.Flights.Flight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DestinationCityId")
                        .HasColumnType("uuid");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<Guid>("OriginCityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DestinationCityId");

                    b.HasIndex("OriginCityId");

                    b.ToTable("Flights", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("360fa856-849a-489a-9e4c-ffe5523ef997"),
                            ArrivalTime = new DateTime(2023, 12, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            DepartureTime = new DateTime(2023, 12, 1, 8, 0, 0, 0, DateTimeKind.Utc),
                            DestinationCityId = new Guid("6b487064-b471-47ce-87c2-257118658842"),
                            FlightNumber = "001",
                            OriginCityId = new Guid("975ed3c2-16a7-48f8-bb26-a485d00c6b5c")
                        },
                        new
                        {
                            Id = new Guid("91a9dd63-2243-45d4-88ca-b0509f634989"),
                            ArrivalTime = new DateTime(2023, 12, 2, 14, 0, 0, 0, DateTimeKind.Utc),
                            DepartureTime = new DateTime(2023, 12, 2, 10, 0, 0, 0, DateTimeKind.Utc),
                            DestinationCityId = new Guid("e2e25b32-f813-46b9-9789-f1808589e98a"),
                            FlightNumber = "002",
                            OriginCityId = new Guid("0fc9af32-a83b-49d0-96a3-0a129959a765")
                        },
                        new
                        {
                            Id = new Guid("ca885c66-249c-4c7c-b4d5-cc192986fcce"),
                            ArrivalTime = new DateTime(2023, 12, 3, 13, 0, 0, 0, DateTimeKind.Utc),
                            DepartureTime = new DateTime(2023, 12, 3, 9, 0, 0, 0, DateTimeKind.Utc),
                            DestinationCityId = new Guid("975ed3c2-16a7-48f8-bb26-a485d00c6b5c"),
                            FlightNumber = "003",
                            OriginCityId = new Guid("3a8c1e28-f627-4ca5-872c-1fb922bdbd88")
                        });
                });

            modelBuilder.Entity("Challenge.Domain.Entities.OutBoxes.Outbox", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("EventData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Processed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("SaveDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Outbox", (string)null);
                });

            modelBuilder.Entity("Challenge.Domain.Entities.Reservations.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("SaveDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("Reservations", (string)null);
                });

            modelBuilder.Entity("Challenge.Domain.Entities.Flights.Flight", b =>
                {
                    b.HasOne("Challenge.Domain.Entities.Cities.City", "DestinationCity")
                        .WithMany()
                        .HasForeignKey("DestinationCityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Challenge.Domain.Entities.Cities.City", "OriginCity")
                        .WithMany()
                        .HasForeignKey("OriginCityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationCity");

                    b.Navigation("OriginCity");
                });

            modelBuilder.Entity("Challenge.Domain.Entities.Reservations.Reservation", b =>
                {
                    b.HasOne("Challenge.Domain.Entities.Flights.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Flight");
                });
#pragma warning restore 612, 618
        }
    }
}
