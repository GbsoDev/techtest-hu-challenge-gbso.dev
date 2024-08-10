using Challenge.Domain.Entities.OutBoxes;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class AddNewReservationToOutboxService
	{
		private readonly ILogger<AddNewReservationToOutboxService> _logger;
		private readonly Lazy<IOutboxRepository> _outboxRepository;
		private readonly Lazy<IDateTimeProvider> _dateTimeProvider;

		public AddNewReservationToOutboxService(ILogger<AddNewReservationToOutboxService> logger, Lazy<IOutboxRepository> outboxRepository, Lazy<IDateTimeProvider> dateTimeProvider)
		{
			_logger = logger;
			_outboxRepository = outboxRepository;
			_dateTimeProvider = dateTimeProvider;
		}

		public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken)
		{
			_logger.LogInformation(LogMessages.CreatingReservation);

			var outbox = new Outbox(EventType.Create, reservation, _dateTimeProvider.Value.UtcNow);
			var outboxSaved = await _outboxRepository.Value.SaveAsync(outbox, cancellationToken);
			await _outboxRepository.Value.SaveChangesAsync(cancellationToken);

			_logger.LogInformation(LogMessages.ReservationCreated, outboxSaved.Id);
		}
	}
}
