using Challenge.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.EfStorage.Configurations
{
	public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
	{
		public void Configure(EntityTypeBuilder<Reservation> builder)
		{
			builder.ToTable("Reservations");

			builder.HasKey(r => r.Id);

			builder.Property(r => r.UserName)
				.IsRequired()
				.HasMaxLength(ReservationParameters.UserNameMaxLength);

			builder.Property(r => r.PassportNumber)
				.IsRequired()
				.HasMaxLength(ReservationParameters.PassportNumberMaxLength);

			builder.Property(r => r.Email)
				.IsRequired()
				.HasMaxLength(ReservationParameters.EmailMaxLength);

			builder.Property(f => f.SeatNumber)
				.IsRequired()
				.HasMaxLength(ReservationParameters.SeatNumberMaxLength);


			builder.Property(r => r.FlightId)
				.IsRequired();

			builder.HasOne(r => r.Flight)
				.WithMany()
				.HasForeignKey(r => r.FlightId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
