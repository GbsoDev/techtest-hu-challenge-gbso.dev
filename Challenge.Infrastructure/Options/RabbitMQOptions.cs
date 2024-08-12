using Challenge.Domain.Options;

namespace Challenge.Infrastructure.Options
{
	[Option("RabbitMQ")]
	public class RabbitMQOptions
	{
		public required string HostName { get; init; }
		public required string UserName { get; init; }
		public required string Password { get; init; }
	}

}
