
namespace Challenge.Domain.Ports
{
	public interface IBrokerService : IDisposable
	{
		Task PublisMessageAtBroker(string messageBody, CancellationToken cancellationToken);
		Task ReadMessageFromBroker(CancellationToken cancellationToken);
	}
}
