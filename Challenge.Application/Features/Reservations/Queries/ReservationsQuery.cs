using MediatR;

namespace Challenge.Application.Features.Reservations.Queries
{
	public class ReservationsQuery : IRequest<IEnumerable<ReservationDto>>
	{
	}
}
