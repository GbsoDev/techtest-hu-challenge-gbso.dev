using Challenge.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.EfStorage
{
	public static class EfStorageProvider
	{
		public static IServiceCollection AddGenericRepository(this IServiceCollection services)
		{
			services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
			return services;
		}

		public static IServiceProvider MigrateDataBase(this IServiceProvider service)
		{
			using (var scope = service.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var dbContext = services.GetRequiredService<IEfContext>();
					dbContext.Database.Migrate();
				}
				catch (Exception ex)
				{
					throw new AppExeption("Error in migration", ex);
				}
			}
			return service;
		}
	}
}
