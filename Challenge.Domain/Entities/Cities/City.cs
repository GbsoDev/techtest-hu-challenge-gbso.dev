namespace Challenge.Domain.Entities.Cities
{
	public class City : DomainEntity<Guid>
	{
		public required string AirportCode { get; init; }
		public required string AirportName { get; init; }
		public required string CityName { get; init; }
	}
}
