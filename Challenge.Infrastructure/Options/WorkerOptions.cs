using Challenge.Domain.Options;

namespace Challenge.Outbox.PublisherWorker.Options
{
	[Option("Worker")]
	public class WorkerOptions
	{
		public int RetryDelayMinutes { get; init; }
		public int ExecutionIntervalHours { get; init; }
		public int ExecutionIntervalMinutes { get; init; }
	}
}
