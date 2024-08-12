using MediatR;

namespace Challenge.Application.Features.Flights.Queries
{
	public record FlightByIdQuery(Guid Id) : IRequest<FlightDto>;
}
