using Challenge.Domain.Options;

namespace Challenge.Infrastructure.Adapters
{
	[Option("SendGrid")]
	public class SendGridOptions
	{
		public required string ApiKey { get; init; }
		public required string FromEmail { get; init; }
		public required string FromName { get; init; }
	}
}
