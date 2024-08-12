using System.Reflection;

namespace Challenge.Infrastructure.Helpers
{
	public static class InfrastructureAssemblyHelper
	{
		public static Assembly GetInfrastructureAssembly => Assembly.GetExecutingAssembly();
	}
}
