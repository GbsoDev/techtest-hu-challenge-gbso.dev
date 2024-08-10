using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Challenge.Infrastructure.Adapters
{
	public class NotifycationService : IDisposable
	{
		private readonly IConnection _connection;
		private readonly IModel _channel;

		public NotifycationService()
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();

			_channel.QueueDeclare(queue: "tu_cola", durable: false, exclusive: false, autoDelete: false, arguments: null);
		}
		protected async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				// Procesa el mensaje aquí
				Console.WriteLine($"[x] Received {message}");
			};

			_channel.BasicConsume(queue: "tu_cola", autoAck: true, consumer: consumer);

			await Task.CompletedTask;
		}

		public void Dispose()
		{
			_channel.Close();
			_connection.Close();
		}
	}
}
