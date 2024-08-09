
namespace Challenge.Domain.Ports
{
	public interface INotifycationService : IDisposable
	{
		Task PublisMessageAtBroker(CancellationToken cancellationToken, string messageBody);
		Task ReadMessageFromBroker(CancellationToken cancellationToken);
	}
}
