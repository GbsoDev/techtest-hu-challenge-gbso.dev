using Challenge.Domain.Entities.Flights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.EfStorage.Configurations
{
	public class FlightConfiguration : IEntityTypeConfiguration<Flight>
	{
		public void Configure(EntityTypeBuilder<Flight> builder)
		{
			builder.ToTable("Flights");

			builder.HasKey(f => f.Id);

			builder.Property(f => f.FlightNumber)
				.IsRequired()
				.HasMaxLength(FlightParameters.FlightNumberMaxLength);

			builder.HasOne(f => f.OriginCity)
				.WithMany()
				.HasForeignKey(f => f.OriginCityId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.HasOne(f => f.DestinationCity)
				.WithMany()
				.HasForeignKey(f => f.DestinationCityId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			Seed(builder);
		}

		private void Seed(EntityTypeBuilder<Flight> builder)
		{
			builder.HasData(
				new
				{
					Id = new Guid("360fa856-849a-489a-9e4c-ffe5523ef997"),
					ArrivalTime = new DateTime(2023, 12, 1, 12, 0, 0, 0, DateTimeKind.Utc),
					DepartureTime = new DateTime(2023, 12, 1, 8, 0, 0, 0, DateTimeKind.Utc),
					DestinationCityId = new Guid("6b487064-b471-47ce-87c2-257118658842"),
					FlightNumber = "001",
					OriginCityId = new Guid("975ed3c2-16a7-48f8-bb26-a485d00c6b5c"),
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
		}
	}
}
