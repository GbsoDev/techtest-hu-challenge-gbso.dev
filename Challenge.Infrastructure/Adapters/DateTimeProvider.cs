using Challenge.Domain.Ports;

namespace Challenge.Infrastructure.Adapters
{
	[Adapter]
	public class DateTimeProvider : IDateTimeProvider
	{
		private readonly TimeZoneInfo _colombiaTimeZone;

		public DateTimeProvider()
		{
			_colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
		}

		public TimeZoneInfo localTimeZone => _colombiaTimeZone;

		public DateTime UtcNow => DateTime.UtcNow;

		public DateTime Now => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _colombiaTimeZone);

		public DateTime Today => Now.Date;
	}
}
