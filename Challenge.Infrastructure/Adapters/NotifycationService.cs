using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Challenge.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Challenge.Infrastructure.Adapters
{
	[Adapter]
	public class NotifycationService : IBrokerService
	{
		private readonly ILogger<NotifycationService> _logger;
		private readonly IOptions<RabbitMQOptions> _rabbitMQOptions;

		private readonly IConnection _connection;
		private readonly IModel _channel;

		public NotifycationService(ILogger<NotifycationService> logger, IOptions<RabbitMQOptions> rabbitMQOptions)
		{
			var factory = CreateConnectionFactory();
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();

			_channel.QueueDeclare(queue: nameof(Reservation), durable: false, exclusive: false, autoDelete: false, arguments: null);
			_logger = logger;
			_rabbitMQOptions = rabbitMQOptions;
		}

		public async Task PublisMessageAtBroker(CancellationToken cancellationToken, string messageBody)
		{
			var body = Encoding.UTF8.GetBytes(messageBody);

			if (!cancellationToken.IsCancellationRequested)
			{
				_channel.BasicPublish(exchange: "",
					routingKey: "hello",
					basicProperties: null,
					body: body);
			}
			await Task.CompletedTask;
		}

		public async Task ReadMessageFromBroker(CancellationToken cancellationToken)
		{
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				// Procesa el mensaje aquí
			};

			_channel.BasicConsume(queue: nameof(Reservation), autoAck: true, consumer: consumer);

			await Task.CompletedTask;
		}

		private ConnectionFactory CreateConnectionFactory()
		{
			return new ConnectionFactory()
			{
				HostName = _rabbitMQOptions.Value.HostName,
				UserName = _rabbitMQOptions.Value.UserName,
				Password = _rabbitMQOptions.Value.Password
			};
		}

		public void Dispose()
		{
			_channel.Close();
			_connection.Close();
		}
	}
}
