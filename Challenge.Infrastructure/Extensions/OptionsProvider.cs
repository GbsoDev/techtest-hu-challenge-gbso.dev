using Challenge.Domain.Exceptions;
using Challenge.Domain.Helpers;
using Challenge.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Challenge.Infrastructure.Extensions
{
	public static class OptionsProvider
	{
		private static Action<BinderOptions> BinderOptions => options => options.BindNonPublicProperties = true;

		public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
		{
			var types = DomainAssemblyHelper.GeyTypesByAttribute(assembly, typeof(OptionAttribute));
			foreach (var type in types)
			{
				var attribute = type.GetCustomAttribute<OptionAttribute>()!;
				var constructor = type.GetConstructor([]);
				if (constructor != null)
				{
					services.ConfigureOption(type, configuration, attribute.SecctionName);
				}
				else
				{
					throw new AppExeption($"Error: The options class {type.FullName} requires an empty constructor");
				}
			}
			return services;
		}

		public static void ConfigureOption(this IServiceCollection services, Type type, IConfiguration configuration, string sectionName)
		{
			const string methodName = "ConfigureOption";
			var classType = typeof(OptionsProvider);

			MethodInfo? configureMethod = classType
				.GetMethod(
					"ConfigureOption",
					BindingFlags.Static | BindingFlags.Public,
					null,
					[typeof(IServiceCollection), typeof(IConfiguration), typeof(string)],
					null
				);

			if (configureMethod != null)
			{
				configureMethod.MakeGenericMethod(type)
					.Invoke(null, [services, configuration, sectionName]);
			}
			else
			{
				throw new AppExeption($"Error: Generic method {classType.FullName}.{methodName} not found");
			}
		}

		public static void ConfigureOption<TOptions>(IServiceCollection services, IConfiguration configuration, string secctionName)
			where TOptions : class, new()
		{
			var configurationSection = configuration.GetSection(secctionName);
			if (configurationSection.Exists())
			{
				services.Configure<TOptions>(option => configurationSection.Bind(option, BinderOptions));
			}
		}
	}
}