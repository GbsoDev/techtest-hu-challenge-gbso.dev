using Challenge.Domain.Services.Reservations;
using MediatR;

namespace Challenge.Application.Features.Reservations.Commands
{
	public class PublishProcessedReservationsAtBrokerCommandHandler : IRequestHandler<PublishProcessedReservationsAtBrokerCommand>
	{
		private readonly Lazy<EventNotificationToBrokerService> _eventNotificationToBrokerService;

		public PublishProcessedReservationsAtBrokerCommandHandler(Lazy<EventNotificationToBrokerService> eventNotificationToBrokerService)
		{
			_eventNotificationToBrokerService = eventNotificationToBrokerService;
		}

		public async Task Handle(PublishProcessedReservationsAtBrokerCommand request, CancellationToken cancellationToken)
		{
			await _eventNotificationToBrokerService.Value.NotifyAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
