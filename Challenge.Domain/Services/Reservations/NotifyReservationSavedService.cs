using Challenge.Domain.Dtos;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class NotifyReservationSavedService
	{
		ILogger<NotifyReservationSavedService> _logger;
		Lazy<IBrokerService> _brokerService;

		public NotifyReservationSavedService(ILogger<NotifyReservationSavedService> logger, Lazy<IBrokerService> brokerService)
		{
			_logger = logger;
			_brokerService = brokerService;
		}

		public async Task NotifyAsync(Reservation reservation, CancellationToken cancellationToken)
		{
			var template = TemplateParameters.CreateReservationEmailBody;

			var body = template
				.Replace(TemplateParameters.UserName, reservation.UserName)
				.Replace(TemplateParameters.ReservationId, reservation.Id.ToString())
				.Replace(TemplateParameters.DepartureTime, reservation.Flight!.DepartureTime.ToString())
				.Replace(TemplateParameters.OriginCityName, reservation.Flight!.OriginCity!.CityName)
				.Replace(TemplateParameters.DestinationCityName, reservation.Flight!.DestinationCity!.CityName)
				.Replace(TemplateParameters.SeatNumber, reservation.SeatNumber);

			var emailDto = new EmailDto
			{
				Email = reservation.Email,
				Subject = TemplateParameters.CreateReservationEmailSubject,
				Body = body
			};

			var message = JsonSerializer.Serialize(emailDto);

			await _brokerService.Value.PublisMessageAtBroker(message, cancellationToken).ConfigureAwait(false);
		}
	}
}
