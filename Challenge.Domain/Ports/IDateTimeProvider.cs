namespace Challenge.Domain.Ports
{
	public interface IDateTimeProvider
	{
		TimeZoneInfo localTimeZone { get; }
		DateTime UtcNow { get; }
		DateTime Now { get; }
		DateTime Today { get; }
	}
}
