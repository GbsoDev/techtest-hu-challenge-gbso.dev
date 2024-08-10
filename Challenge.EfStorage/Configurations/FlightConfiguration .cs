using Challenge.Domain.Entities.Flights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.EfStorage.Configurations
{
	public class FlightConfiguration : IEntityTypeConfiguration<Flight>
	{
		public void Configure(EntityTypeBuilder<Flight> builder)
		{
			builder.HasKey(f => f.Id);
			builder.ToTable("Flights");

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
		}
	}
}
