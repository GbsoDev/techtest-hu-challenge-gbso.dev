using System.Reflection;

namespace Challenge.Outbox.PublisherWorker.Helpers;

public static class WorkerAssemblyHelper
{
	public static Assembly GetWorkerAssembly => Assembly.GetExecutingAssembly();
}
