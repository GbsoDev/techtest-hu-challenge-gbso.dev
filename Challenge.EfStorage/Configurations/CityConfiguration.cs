using Challenge.Domain.Entities.Cities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.EfStorage.Configurations
{
	public class CityConfiguration : IEntityTypeConfiguration<City>
	{
		public void Configure(EntityTypeBuilder<City> builder)
		{
			builder.ToTable("Cities");
			builder.HasKey(c => c.Id);

			builder
				.Property(c => c.AirportCode)
				.IsRequired()
				.HasMaxLength(CityParameters.AirportCodeMaxLength);

			builder
				.Property(c => c.AirportName)
				.IsRequired()
				.HasMaxLength(CityParameters.AirportNameMaxLength);

			builder
				.Property(c => c.CityName)
				.IsRequired()
				.HasMaxLength(CityParameters.CityNameMaxLength);

			Seed(builder);
		}

		public static void Seed(EntityTypeBuilder<City> builder)
		{
			builder.HasData(
				new { Id = Guid.Parse("975ed3c2-16a7-48f8-bb26-a485d00c6b5c"), AirportCode = "SDQ", AirportName = "Aeropuerto Internacional de Las Américas", CityName = "Santo Domingo" },
				new { Id = Guid.Parse("0fc9af32-a83b-49d0-96a3-0a129959a765"), AirportCode = "PUJ", AirportName = "Aeropuerto Internacional de Punta Cana", CityName = "Punta Cana" },
				new { Id = Guid.Parse("e2e25b32-f813-46b9-9789-f1808589e98a"), AirportCode = "MCO", AirportName = "Aeropuerto Internacional de Orlando", CityName = "Orlando" },
				new { Id = Guid.Parse("3a8c1e28-f627-4ca5-872c-1fb922bdbd88"), AirportCode = "MIA", AirportName = "Aeropuerto Internacional de Miami", CityName = "Miami" },
				new { Id = Guid.Parse("6b487064-b471-47ce-87c2-257118658842"), AirportCode = "JFK", AirportName = "Aeropuerto Internacional John F. Kennedy", CityName = "Nueva York" },
				new { Id = Guid.Parse("369d93a7-ebdc-4f14-8a70-4f28164303c3"), AirportCode = "FLL", AirportName = "Aeropuerto Internacional de Fort Lauderdale-Hollywood", CityName = "Fort Lauderdale" },
				new { Id = Guid.Parse("c2f4a542-df4b-4997-be91-7140827bca80"), AirportCode = "MDE", AirportName = "Aeropuerto Internacional José María Córdova", CityName = "Medellín" }
			);

		}
	}
}
