using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class UpdateReservationInStorageService
	{
		private readonly ILogger<UpdateReservationInStorageService> _logger;
		private readonly Lazy<IReservationsRepository> _reservationsRepository;

		public UpdateReservationInStorageService(ILogger<UpdateReservationInStorageService> logger, Lazy<IReservationsRepository> reservationsRepository)
		{
			_logger = logger;
			_reservationsRepository = reservationsRepository;
		}

		public async Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation(LogMessages.UpdatingObject, nameof(Reservation));
			var reservationUpdated = await _reservationsRepository.Value.UpdateAsync(reservation).ConfigureAwait(false);
			await _reservationsRepository.Value.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
			_logger.LogInformation(LogMessages.ObjectUpdated, nameof(Reservation), reservationUpdated.Id);
		}
	}
}
