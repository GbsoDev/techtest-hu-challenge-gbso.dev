using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class DeleteReservationInStorageService
	{
		private readonly ILogger<UpdateReservationInStorageService> _logger;
		private readonly Lazy<IReservationsRepository> _reservationsRepository;

		public DeleteReservationInStorageService(ILogger<UpdateReservationInStorageService> logger, Lazy<IReservationsRepository> reservationsRepository)
		{
			_logger = logger;
			_reservationsRepository = reservationsRepository;
		}

		public async Task DeleteAsync(Reservation reservation, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation(LogMessages.DeletingObject, nameof(Reservation));
			var deleted = await _reservationsRepository.Value.DeleteByIdAsync(reservation.Id).ConfigureAwait(false);
			if (deleted)
			{
				await _reservationsRepository.Value.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
				_logger.LogInformation(LogMessages.ObjectDeleted, nameof(Reservation), reservation.Id);
			}
			else
			{
				_logger.LogWarning(ValidationMessages.NotFound, nameof(Reservation), reservation.Id);
			}
		}
	}
}
