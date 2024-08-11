using Challenge.Domain.Dtos;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class NotifyBrokerMessageToEmailService
	{
		ILogger<NotifyBrokerMessageToEmailService> _logger;
		Lazy<IBrokerService> _brokerService;
		Lazy<IEmailService> _emailService;

		public NotifyBrokerMessageToEmailService(ILogger<NotifyBrokerMessageToEmailService> logger, Lazy<IBrokerService> brokerService, Lazy<IEmailService> emailService)
		{
			_logger = logger;
			_brokerService = brokerService;
			_emailService = emailService;
		}

		public async Task NotifyAsync(CancellationToken cancellationToken)
		{
			var brokerMessage = await _brokerService.Value.ReadMessageFromBroker(cancellationToken).ConfigureAwait(false);
			var emailRecipient = JsonSerializer.Deserialize<EmailDto>(brokerMessage);
			await _emailService.Value.SendEmailAsync(emailRecipient!, cancellationToken).ConfigureAwait(false);
		}
	}
}
