using Challenge.Domain.Services.Reservations;
using MediatR;

namespace Challenge.Application.Features.Reservations.Commands
{
	public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand>
	{
		private readonly Lazy<AddCancelReservationToOutboxService> _cancelReservationService;

		public CancelReservationCommandHandler(Lazy<AddCancelReservationToOutboxService> cancelReservationService)
		{
			this._cancelReservationService = cancelReservationService;
		}

		public async Task Handle(CancelReservationCommand request, CancellationToken cancellationToken)
		{
			var reservationId = request.Id;
			await _cancelReservationService.Value.CancelAsync(reservationId, cancellationToken);
		}
	}
}
