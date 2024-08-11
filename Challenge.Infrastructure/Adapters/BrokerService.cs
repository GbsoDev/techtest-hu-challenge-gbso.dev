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
	public class BrokerService : IBrokerService
	{
		private readonly ILogger<BrokerService> _logger;
		private readonly IOptions<RabbitMQOptions> _rabbitMQOptions;

		private readonly IConnection _connection;
		private readonly IModel _channel;

		public BrokerService(ILogger<BrokerService> logger, IOptions<RabbitMQOptions> rabbitMQOptions)
		{
			_logger = logger;
			_rabbitMQOptions = rabbitMQOptions;

			var factory = CreateConnectionFactory();
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			QueueDeclare();
		}

		private void QueueDeclare()
		{
			_channel.QueueDeclare(
				queue: nameof(Reservation),
				durable: true,
				exclusive: false,
				autoDelete: false,
				arguments: null);
		}

		public async Task PublisMessageAtBroker(string messageBody, CancellationToken cancellationToken)
		{
			var body = Encoding.UTF8.GetBytes(messageBody);

			if (!cancellationToken.IsCancellationRequested)
			{
				_channel.BasicPublish(
					exchange: string.Empty,
					routingKey: nameof(Reservation),
					basicProperties: null,
					body: body);
			}
			await Task.CompletedTask;
		}

		public async Task<string> ReadMessageFromBroker(CancellationToken cancellationToken)
		{
			var tcs = new TaskCompletionSource<string>();
			var consumer = new EventingBasicConsumer(_channel);

			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				tcs.TrySetResult(message);
				_channel.BasicAck(ea.DeliveryTag, false); // Reconocer el mensaje
			};

			_channel.BasicConsume(queue: nameof(Reservation), autoAck: false, consumer: consumer);

			using (cancellationToken.Register(() => tcs.TrySetCanceled()))
			{
				return await tcs.Task;
			}
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
