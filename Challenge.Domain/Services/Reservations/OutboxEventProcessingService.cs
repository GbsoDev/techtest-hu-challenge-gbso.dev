using Challenge.Domain.Entities.OutBoxes;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class OutboxEventProcessingService
	{
		private readonly ILogger<AddCancelReservationToOutboxService> _logger;
		private readonly Lazy<IOutboxRepository> _outboxRepository;
		private readonly Lazy<IReservationsRepository> _reservationsRepository;
		private readonly Lazy<IFlightsRepository> _flightsRepository;
		private readonly Lazy<SaveReservationToStorageService> _saveReservationToStorageService;
		private readonly Lazy<UpdateReservationInStorageService> _updateReservationInStorageService;
		private readonly Lazy<DeleteReservationInStorageService> _deleteReservationInStorageService;

		public OutboxEventProcessingService(
			ILogger<AddCancelReservationToOutboxService> logger,
			Lazy<IOutboxRepository> outboxRepository,
			Lazy<IReservationsRepository> reservationsRepository,
			Lazy<IFlightsRepository> flightsRepository,
			Lazy<SaveReservationToStorageService> saveReservationToStorageService,
			Lazy<UpdateReservationInStorageService> updateReservationToStorageService,
			Lazy<DeleteReservationInStorageService> deleteReservationInStorageService)
		{
			_logger = logger;
			_outboxRepository = outboxRepository;
			_reservationsRepository = reservationsRepository;
			_saveReservationToStorageService = saveReservationToStorageService;
			_updateReservationInStorageService = updateReservationToStorageService;
			_deleteReservationInStorageService = deleteReservationInStorageService;
			_flightsRepository = flightsRepository;
		}

		public async Task ProcessAsync(CancellationToken cancellationToken = default)
		{
			IEnumerable<Outbox> unProcessedOutboxes = await _outboxRepository.Value.GetAllUnProcessedAsync(cancellationToken);

			_logger.LogInformation(LogMessages.ProcessingObjects, nameof(Reservation));
			await SetReservationData(unProcessedOutboxes, cancellationToken);

			foreach (var outbox in unProcessedOutboxes)
			{
				var task = outbox.EventType switch
				{
					EventType.Create => ProcessReservationEventAsync(outbox, _saveReservationToStorageService.Value.SaveAsync, cancellationToken),
					EventType.Update => ProcessReservationEventAsync(outbox, _updateReservationInStorageService.Value.UpdateAsync, cancellationToken),
					EventType.Delete => ProcessReservationEventAsync(outbox, _deleteReservationInStorageService.Value.DeleteAsync, cancellationToken),
					_ => throw new InvalidOperationException(string.Format(ValidationMessages.NotFoundEnumValue, outbox.EventType, nameof(EventType)))
				};
				await task.ConfigureAwait(false);
			}
			_logger.LogInformation(LogMessages.ObjectsProcessed, nameof(Reservation));
		}

		private async Task SetReservationData(IEnumerable<Outbox> unProcessedOutboxes, CancellationToken cancellationToken)
		{
			var deletIds = unProcessedOutboxes.Where(outbox => outbox.EventType == EventType.Delete)
				.Select(outbox => outbox.DeserializedData<Guid>())
				.ToArray();

			var reservationsToDelete = await _reservationsRepository.Value.GetMultipleByIdAndIncludeAnyAsync(deletIds, cancellationToken);

			var joinToDelete = unProcessedOutboxes.Join(reservationsToDelete,
				outbox => outbox.EventType == EventType.Delete ? outbox.DeserializedData<Guid>() : outbox.DeserializedData<Reservation>().Id,
				reservation => reservation.Id,
				(outbox, reservation) => (outbox, reservation)).ToArray();

			foreach (var item in joinToDelete)
			{
				item.outbox.SerializeAndSetEventData(item.reservation);
			}

			var otherUnProcessedOutboxes = unProcessedOutboxes.Where(outbox => outbox.EventType != EventType.Delete)
				.ToArray();

			var flightIds = otherUnProcessedOutboxes
				.Select(outbox => outbox.DeserializedData<Reservation>().FlightId)
				.ToArray();

			var flights = await _flightsRepository.Value.GetMultipleByIds(flightIds, cancellationToken);

			var otherJoin = otherUnProcessedOutboxes.Join(flights,
				outbox => outbox.DeserializedData<Reservation>().FlightId,
				flight => flight.Id,
				(outbox, flight) => (outbox, flight));

			foreach (var item in otherJoin)
			{
				var eventData = item.outbox.DeserializedData<Reservation>();
				eventData.SetFlight(item.flight);
				item.outbox.SerializeAndSetEventData(eventData);
			}
		}

		private async Task ProcessReservationEventAsync(
			Outbox outbox,
			Func<Reservation, CancellationToken, Task> processReservationAsync,
			CancellationToken cancellationToken)
		{
			var reservation = outbox.DeserializedData<Reservation>();
			await processReservationAsync(reservation!, cancellationToken).ConfigureAwait(false);
			outbox.MarkAsProcessed();
			await _outboxRepository.Value.UpdateAsync(outbox, cancellationToken).ConfigureAwait(false);
			await _outboxRepository.Value.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
