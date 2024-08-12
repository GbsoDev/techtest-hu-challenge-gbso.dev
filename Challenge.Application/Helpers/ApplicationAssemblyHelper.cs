using System.Reflection;

namespace Challenge.Application.Helpers
{
	public static class ApplicationAssemblyHelper
	{
		public static Assembly GetApplicationAssembly => Assembly.GetExecutingAssembly();
	}
}
