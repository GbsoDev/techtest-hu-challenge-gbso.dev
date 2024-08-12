namespace Challenge.Application.Features
{
	public class ReservationDto
	{
		public Guid Id { get; init; }
		public required string UserName { get; init; }
		public required string PassportNumber { get; init; }
		public required string Email { get; init; }
		public required Guid FlightId { get; init; }
		public string? FlightCode { get; init; }
		public required string SeatNumber { get; init; }

	}
}
