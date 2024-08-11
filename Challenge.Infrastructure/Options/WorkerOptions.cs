using Challenge.Domain.Options;

namespace Challenge.Outbox.PublisherWorker.Options
{
	[Option("Worker")]
	public class WorkerOptions
	{

		public int NumberOfRetries {get;init;}
		public TimeSpan RetryDelay {get;init;}
		public TimeSpan ExecutionInterval { get; init;}
	}
}
