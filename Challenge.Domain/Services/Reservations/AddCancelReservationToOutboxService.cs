using Challenge.Domain.Entities.OutBoxes;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Challenge.Domain.Validations;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class AddCancelReservationToOutboxService
	{
		private readonly ILogger<AddCancelReservationToOutboxService> _logger;
		private readonly Lazy<IOutboxRepository> _outboxRepository;
		private readonly Lazy<IDateTimeProvider> _dateTimeProvider;

		public AddCancelReservationToOutboxService(ILogger<AddCancelReservationToOutboxService> logger, Lazy<IOutboxRepository> outboxRepository, Lazy<IDateTimeProvider> dateTimeProvider)
		{
			_logger = logger;
			_outboxRepository = outboxRepository;
			_dateTimeProvider = dateTimeProvider;
		}

		public async Task CancelAsync(Guid id, CancellationToken cancellationToken)
		{
			_logger.LogInformation(LogMessages.CancellingReservation);

			if (!id.IsNotNull() || !id.IsNotDefault())
			{
				_logger.LogError(ValidationMessages.EmptyOrNullData, nameof(Reservation.Id));
				throw new ValidationException(string.Format(ValidationMessages.EmptyOrNullData, nameof(Reservation.Id)));
			}

			var outbox = new Outbox(EventType.Delete, id, _dateTimeProvider.Value.UtcNow);
			var outboxSaved = await _outboxRepository.Value.SaveAsync(outbox, cancellationToken);
			await _outboxRepository.Value.SaveChangesAsync(cancellationToken);

			_logger.LogInformation(LogMessages.ReservationCancelled, outboxSaved.Id);
		}
	}
}
