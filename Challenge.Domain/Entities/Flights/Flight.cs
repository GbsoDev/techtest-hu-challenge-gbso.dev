using Challenge.Domain.Entities.Cities;

namespace Challenge.Domain.Entities.Flights
{
	public class Flight : DomainEntity<Guid>
	{
		public Guid OriginCityId { get; init; }
		public Guid DestinationCityId { get; init; }
		public required string FlightNumber { get; init; }
		public DateTime DepartureTime { get; init; }
		public DateTime ArrivalTime { get; init; }
		public virtual City? DestinationCity { get; init; }
		public virtual City? OriginCity { get; init; }
		public string FlightCode => $"{OriginCity?.AirportCode}-{DestinationCity?.AirportCode}-{FlightNumber}";
	}
}
