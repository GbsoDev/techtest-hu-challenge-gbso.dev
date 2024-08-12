namespace Challenge.Infrastructure.Adapters
{
	using Challenge.Domain.Dtos;
	using Challenge.Domain.Ports;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using SendGrid;
	using SendGrid.Helpers.Mail;
	using System.Threading.Tasks;

	[Adapter]
	public class EmailService : IEmailService
	{
		private readonly ILogger<EmailService> _logger;
		private readonly SendGridOptions _sendGridOptions;

		public EmailService(ILogger<EmailService> logger, IOptions<SendGridOptions> sendGridOptions)
		{
			_logger = logger;
			_sendGridOptions = sendGridOptions.Value;
		}

		public async Task SendEmailAsync(EmailDto emailRecipient, CancellationToken cancellationToken)
		{
			var client = new SendGridClient(_sendGridOptions.ApiKey);
			var from = new EmailAddress(_sendGridOptions.FromEmail, _sendGridOptions.FromName);
			var to = new EmailAddress(emailRecipient.Email);
			var htmlContent = $"<strong>{emailRecipient.Body}</strong>";
			var msg = MailHelper.CreateSingleEmail(from, to, emailRecipient.Subject, emailRecipient.Body, htmlContent);
			var response = await client.SendEmailAsync(msg, cancellationToken);
		}
	}
}
