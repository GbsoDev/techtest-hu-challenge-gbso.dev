using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class SaveReservationToStorageService
	{
		private readonly ILogger<SaveReservationToStorageService> _logger;
		private readonly Lazy<IReservationsRepository> _reservationsRepository;

		public SaveReservationToStorageService(ILogger<SaveReservationToStorageService> logger, Lazy<IReservationsRepository> reservationsRepository)
		{
			_logger = logger;
			_reservationsRepository = reservationsRepository;
		}

		public async Task SaveAsync(Reservation reservation, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation(LogMessages.SavingObject, nameof(Reservation));
			var reservationSaved = await _reservationsRepository.Value.SaveAsync(reservation).ConfigureAwait(false);
			await _reservationsRepository.Value.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
			_logger.LogInformation(LogMessages.ObjectSaved, nameof(Reservation), reservationSaved.Id);
		}
	}
}
