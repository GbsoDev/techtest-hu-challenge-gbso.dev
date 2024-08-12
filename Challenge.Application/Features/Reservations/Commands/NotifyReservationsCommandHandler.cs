using Challenge.Domain.Services.Reservations;
using MediatR;

namespace Challenge.Application.Features.Reservations.Commands
{
	public class NotifyReservationsCommandHandler : IRequestHandler<NotifyReservationsCommand>
	{
		private Lazy<NotifyBrokerMessageToEmailService> _notifyBrokerMessageToEmailService;

		public NotifyReservationsCommandHandler(Lazy<NotifyBrokerMessageToEmailService> notifyBrokerMessageToEmailService)
		{
			_notifyBrokerMessageToEmailService = notifyBrokerMessageToEmailService;
		}

		public async Task Handle(NotifyReservationsCommand request, CancellationToken cancellationToken)
		{
			await _notifyBrokerMessageToEmailService.Value.NotifyAsync(cancellationToken);
		}
	}
}
