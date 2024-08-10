using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Infrastructure.Extensions
{
	public static class LazyLoadingProvider
	{
		public static IServiceCollection AddLazyLoadingSupport(this IServiceCollection services)
		{
			services.AddTransient(typeof(Lazy<>), typeof(LazyLoadingProvider<>));
			return services;
		}
	}

	public class LazyLoadingProvider<T> : Lazy<T>
		where T : notnull
	{
		public LazyLoadingProvider(IServiceProvider serviceProvider) : base(() => serviceProvider.GetRequiredService<T>())
		{
		}
	}
}
