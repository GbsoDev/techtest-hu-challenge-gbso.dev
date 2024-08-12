using Challenge.Domain.Dtos;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class NotifyReservationCancelledService
	{
		ILogger<NotifyReservationCancelledService> _logger;
		Lazy<IBrokerService> _brokerService;

		public NotifyReservationCancelledService(ILogger<NotifyReservationCancelledService> logger, Lazy<IBrokerService> brokerService)
		{
			_logger = logger;
			_brokerService = brokerService;
		}


		public async Task NotifyAsync(Reservation reservation, CancellationToken cancellationToken)
		{
			var template = TemplateParameters.CancelReservationEmailBody;

			var body = template
				.Replace(TemplateParameters.UserName, reservation.UserName)
				.Replace(TemplateParameters.ReservationId, reservation.Id.ToString())
				.Replace(TemplateParameters.DepartureTime, reservation.Flight!.DepartureTime.ToString())
				.Replace(TemplateParameters.OriginCityName, reservation.Flight!.OriginCity!.CityName)
				.Replace(TemplateParameters.DestinationCityName, reservation.Flight!.DestinationCity!.CityName);

			var emailDto = new EmailDto
			{
				Email = reservation.Email,
				Subject = TemplateParameters.CancelReservationEmailSubject,
				Body = body
			};

			var message = JsonSerializer.Serialize(emailDto);

			await _brokerService.Value.PublisMessageAtBroker(message, cancellationToken).ConfigureAwait(false);
		}
	}
}
