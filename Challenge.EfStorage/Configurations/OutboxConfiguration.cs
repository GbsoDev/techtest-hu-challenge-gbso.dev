using Challenge.Domain.Entities.OutBoxes;
using Challenge.EfStorage.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.EfStorage.Configurations
{
	public class OutboxConfiguration : IEntityTypeConfiguration<Outbox>
	{
		public void Configure(EntityTypeBuilder<Outbox> builder)
		{
			builder.ToTable("Outbox");
			builder.HasKey(o => o.Id);

			builder.Property(o => o.EventType)
				.IsRequired()
				.HasConversion<StringEnumConverter<EventType>>();

			builder.Property(o => o.EventData)
				.IsRequired();

			builder.Property(o => o.Processed)
				.IsRequired();
		}
	}
}
