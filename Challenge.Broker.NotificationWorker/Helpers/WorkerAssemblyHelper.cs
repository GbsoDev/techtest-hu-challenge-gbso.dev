using System.Reflection;

namespace Challenge.Broker.NotificationWorker.Helpers;

public static class WorkerAssemblyHelper
{
	public static Assembly GetWorkerAssembly => Assembly.GetExecutingAssembly();
}
