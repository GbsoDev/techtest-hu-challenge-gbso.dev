using System.Reflection;

namespace Challenge.Email.NotificationWorker.Helpers;

public static class WorkerAssemblyHelper
{
	public static Assembly GetWorkerAssembly => Assembly.GetExecutingAssembly();
}
