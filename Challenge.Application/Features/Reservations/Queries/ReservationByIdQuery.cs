using MediatR;

namespace Challenge.Application.Features.Reservations.Queries
{
	public record ReservationByIdQuery(Guid Id) : IRequest<ReservationDto>;
}
