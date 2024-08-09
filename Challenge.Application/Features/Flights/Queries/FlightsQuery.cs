using MediatR;

namespace Challenge.Application.Features.Flights.Queries
{
	public class FlightsQuery : IRequest<IEnumerable<FlightDto>>
	{
	}
}
