using Challenge.Domain.Entities.OutBoxes;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class OutboxEventNotificationService
	{
		private readonly ILogger<AddCancelReservationToOutboxService> _logger;
		private readonly Lazy<IOutboxRepository> _outboxRepository;
		private readonly Lazy<NotifyReservationSavedService> _notifyReservationCreatedService;
		private readonly Lazy<NotifyReservationCancelledService> _notifyReservationCancelledService;
		private readonly Lazy<NotifyReservationUpdatedService> _notifyReservationUpdatedService;

		public OutboxEventNotificationService(ILogger<AddCancelReservationToOutboxService> logger,
			Lazy<IOutboxRepository> outboxRepository,
			Lazy<NotifyReservationSavedService> notifyReservationCreatedService,
			Lazy<NotifyReservationCancelledService> notifyReservationCancelledService,
			Lazy<NotifyReservationUpdatedService> notifyReservationUpdatedService)
		{
			_logger = logger;
			_outboxRepository = outboxRepository;
			_notifyReservationCreatedService = notifyReservationCreatedService;
			_notifyReservationCancelledService = notifyReservationCancelledService;
			_notifyReservationUpdatedService = notifyReservationUpdatedService;
		}

		public async Task NotifyAsync(CancellationToken cancellationToken = default)
		{
			var unProcessedOutboxes = await _outboxRepository.Value.GetAllUnProcessedAsync(cancellationToken);

			_logger.LogInformation(LogMessages.ProcessingObjects, nameof(Reservation));
			foreach (var outbox in unProcessedOutboxes)
			{
				var task = outbox.EventType switch
				{
					EventType.Create => ProcessReservationEventAsync(outbox, _notifyReservationCreatedService.Value.NotifyAsync, cancellationToken),
					EventType.Update => ProcessReservationEventAsync(outbox, _notifyReservationUpdatedService.Value.NotifyAsync, cancellationToken),
					EventType.Delete => ProcessReservationEventAsync(outbox, _notifyReservationCancelledService.Value.NotifyAsync, cancellationToken),
					_ => throw new InvalidOperationException(string.Format(ValidationMessages.NotFoundEnumValue, outbox.EventType, nameof(EventType)))
				};
				await task.ConfigureAwait(false);
			}
			_logger.LogInformation(LogMessages.ObjectsProcessed, nameof(Reservation));
		}

		private async Task ProcessReservationEventAsync(
			Outbox outbox,
			Func<Reservation, CancellationToken, Task> processReservationAsync,
			CancellationToken cancellationToken)
		{
			var reservation = JsonSerializer.Deserialize<Reservation>(outbox.EventData)!;
			await processReservationAsync(reservation!, cancellationToken).ConfigureAwait(false);
			await _outboxRepository.Value.DeleteAsync(outbox, cancellationToken).ConfigureAwait(false);
			await _outboxRepository.Value.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
