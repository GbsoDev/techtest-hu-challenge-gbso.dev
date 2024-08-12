using System.Reflection;

namespace Challenge.EfStorage.Helpers
{
	public static class EfStorageAssemblyHelper
	{
		public static Assembly GetEfStorageAssembly => Assembly.GetExecutingAssembly();
	}
}
