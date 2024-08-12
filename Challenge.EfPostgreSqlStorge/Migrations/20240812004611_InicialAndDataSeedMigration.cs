using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Challenge.EfPostgreSqlStorge.Migrations
{
    /// <inheritdoc />
    public partial class InicialAndDataSeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AirportCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    AirportName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CityName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Outbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventType = table.Column<string>(type: "text", nullable: false),
                    EventData = table.Column<string>(type: "text", nullable: false),
                    Processed = table.Column<bool>(type: "boolean", nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SaveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginCityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DestinationCityId = table.Column<Guid>(type: "uuid", nullable: false),
                    FlightNumber = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Cities_DestinationCityId",
                        column: x => x.DestinationCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Cities_OriginCityId",
                        column: x => x.OriginCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PassportNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FlightId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeatNumber = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SaveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "AirportCode", "AirportName", "CityName" },
                values: new object[,]
                {
                    { new Guid("0fc9af32-a83b-49d0-96a3-0a129959a765"), "PUJ", "Aeropuerto Internacional de Punta Cana", "Punta Cana" },
                    { new Guid("369d93a7-ebdc-4f14-8a70-4f28164303c3"), "FLL", "Aeropuerto Internacional de Fort Lauderdale-Hollywood", "Fort Lauderdale" },
                    { new Guid("3a8c1e28-f627-4ca5-872c-1fb922bdbd88"), "MIA", "Aeropuerto Internacional de Miami", "Miami" },
                    { new Guid("6b487064-b471-47ce-87c2-257118658842"), "JFK", "Aeropuerto Internacional John F. Kennedy", "Nueva York" },
                    { new Guid("975ed3c2-16a7-48f8-bb26-a485d00c6b5c"), "SDQ", "Aeropuerto Internacional de Las Américas", "Santo Domingo" },
                    { new Guid("c2f4a542-df4b-4997-be91-7140827bca80"), "MDE", "Aeropuerto Internacional José María Córdova", "Medellín" },
                    { new Guid("e2e25b32-f813-46b9-9789-f1808589e98a"), "MCO", "Aeropuerto Internacional de Orlando", "Orlando" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "ArrivalTime", "DepartureTime", "DestinationCityId", "FlightNumber", "OriginCityId" },
                values: new object[,]
                {
                    { new Guid("360fa856-849a-489a-9e4c-ffe5523ef997"), new DateTime(2023, 12, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 12, 1, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("6b487064-b471-47ce-87c2-257118658842"), "001", new Guid("975ed3c2-16a7-48f8-bb26-a485d00c6b5c") },
                    { new Guid("91a9dd63-2243-45d4-88ca-b0509f634989"), new DateTime(2023, 12, 2, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 12, 2, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("e2e25b32-f813-46b9-9789-f1808589e98a"), "002", new Guid("0fc9af32-a83b-49d0-96a3-0a129959a765") },
                    { new Guid("ca885c66-249c-4c7c-b4d5-cc192986fcce"), new DateTime(2023, 12, 3, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 12, 3, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("975ed3c2-16a7-48f8-bb26-a485d00c6b5c"), "003", new Guid("3a8c1e28-f627-4ca5-872c-1fb922bdbd88") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationCityId",
                table: "Flights",
                column: "DestinationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_OriginCityId",
                table: "Flights",
                column: "OriginCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightId",
                table: "Reservations",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Outbox");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
