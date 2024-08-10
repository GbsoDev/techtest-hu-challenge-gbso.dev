using Challenge.EfPostgreSqlStorge;
using Challenge.EfStorage;
using Challenge.EfStorage.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Infrastructure.Extensions
{
	public static class StorageProvider
	{
		public static IServiceCollection AddEfStorageProvider(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddPgsqlContext(configuration);
			services.AddGenericRepository();
			services.AddServices<RepositoryAttribute>(EfStorageAssemblyHelper.GetEfStorageAssembly);
			return services;
		}
	}
}
