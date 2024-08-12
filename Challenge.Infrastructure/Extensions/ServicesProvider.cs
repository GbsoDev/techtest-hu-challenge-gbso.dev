using Challenge.Domain.Helpers;
using Challenge.Domain.Services;
using Challenge.Infrastructure.Adapters;
using Challenge.Infrastructure.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Challenge.Infrastructure.Extensions
{
	public static class ServicesProvider
	{
		public static IServiceCollection AddDomainServices(this IServiceCollection services)
		{
			services.AddServices<ServiceAttribute>(DomainAssemblyHelper.GetDomainAssembly);
			return services;
		}

		public static IServiceCollection AddAdapters(this IServiceCollection services)
		{
			services.AddServices<AdapterAttribute>(InfrastructureAssemblyHelper.GetInfrastructureAssembly);
			return services;
		}

		public static IServiceCollection AddServices<TAttribute>(this IServiceCollection services, Assembly assembly)
			where TAttribute : Attribute
		{
			foreach (var type in DomainAssemblyHelper.GeyTypesByAttribute(assembly, typeof(TAttribute)))
			{
				var @interface = type.GetInterface(type.BuildInterfaceName());
				services.AddScoped(type);
				if (@interface != null)
				{
					services.AddScoped(@interface, type);
				}
			}
			return services;
		}
	}
}