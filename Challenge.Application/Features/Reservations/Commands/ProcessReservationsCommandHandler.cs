using Challenge.Domain.Services.Reservations;
using MediatR;

namespace Challenge.Application.Features.Reservations.Commands
{
	public class ProcessReservationsCommandHandler : IRequestHandler<ProcessReservationsCommand>
	{
		private readonly Lazy<OutboxEventProcessingService> _processReservationsService;

		public ProcessReservationsCommandHandler(Lazy<OutboxEventProcessingService> processReservationsService)
		{
			_processReservationsService = processReservationsService;
		}

		public async Task Handle(ProcessReservationsCommand request, CancellationToken cancellationToken)
		{
			await _processReservationsService.Value.ProcessAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
