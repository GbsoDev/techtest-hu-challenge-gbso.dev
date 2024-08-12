using System.Reflection;

namespace Challenge.Domain.Helpers
{
	public static class DomainAssemblyHelper
	{
		public static Assembly GetDomainAssembly => Assembly.GetExecutingAssembly();

		public static IEnumerable<Type> GeyTypesByAttribute(Assembly assembly, Type TipoAtributo)
		{
			var types = assembly.GetTypes()
				.Where(type => type.CustomAttributes.Any(attribute => attribute.AttributeType == TipoAtributo));
			return types;
		}
	}
}
