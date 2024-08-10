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
		}
	}
}
