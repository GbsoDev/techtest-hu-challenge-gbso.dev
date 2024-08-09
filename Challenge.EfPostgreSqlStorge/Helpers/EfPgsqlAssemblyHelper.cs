using System.Reflection;

namespace Challenge.EfPostgreSqlStorge.Helpers
{
	public static class EfPgsqlAssemblyHelper
	{
		public static Assembly GetEfPgsqlStorageAssembly => Assembly.GetExecutingAssembly();
	}
}
