using MediatR;

namespace Challenge.Application.Features.Reservations.Commands
{
	public record class CancelReservationCommand(Guid Id) : IRequest;
}
