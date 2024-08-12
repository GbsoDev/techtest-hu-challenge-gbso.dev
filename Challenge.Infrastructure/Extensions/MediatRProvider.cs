using Challenge.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Challenge.Infrastructure.Extensions
{
	public static class MediatRProvider
	{
		public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
		{
			services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
			return services;
		}
	}
}
