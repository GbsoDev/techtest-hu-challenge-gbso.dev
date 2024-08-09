using MediatR;

namespace Challenge.Application.Features.Reservations.Commands
{
	public class CreateReservationCommand : ReservationDto, IRequest
	{
	}
}
