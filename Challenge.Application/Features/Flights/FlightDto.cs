using Challenge.Domain.Entities;

namespace Challenge.Application.Features.Flights
{
	public class FlightDto : DomainEntity<Guid>
	{
		public Guid Id { get; init; }
		public Guid OriginCityId { get; init; }
		public string? OriginCityName { get; init; }
		public string? OriginAirportName { get; init; }
		public Guid DestinationCityId { get; init; }
		public string? DestinationCityName { get; init; }
		public string? DestinationAirportName { get; init; }
		public required string FlightNumber { get; init; }
		public DateTime DepartureTime { get; init; }
		public DateTime ArrivalTime { get; init; }
		public string? FlightCode { get; init; }
	}
}
