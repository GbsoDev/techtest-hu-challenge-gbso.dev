using Challenge.Domain.Entities.OutBoxes;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class AddUpdateReservationToOutboxService
	{
		private readonly ILogger<AddUpdateReservationToOutboxService> _logger;
		private readonly Lazy<IOutboxRepository> _outboxRepository;
		private readonly Lazy<IDateTimeProvider> _dateTimeProvider;

		public AddUpdateReservationToOutboxService(ILogger<AddUpdateReservationToOutboxService> logger, Lazy<IOutboxRepository> outboxRepository, Lazy<IDateTimeProvider> dateTimeProvider)
		{
			_logger = logger;
			_outboxRepository = outboxRepository;
			_dateTimeProvider = dateTimeProvider;
		}

		public async Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken)
		{
			_logger.LogInformation(LogMessages.UpdatingReservation);

			var outbox = new Outbox(EventType.Update, reservation, _dateTimeProvider.Value.UtcNow);
			var outboxSaved = await _outboxRepository.Value.SaveAsync(outbox, cancellationToken);
			await _outboxRepository.Value.SaveChangesAsync(cancellationToken);

			_logger.LogInformation(LogMessages.ReservationUpdated, outboxSaved.Id);
		}
	}
}
