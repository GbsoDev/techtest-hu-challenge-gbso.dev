using Challenge.EfStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.EfPostgreSqlStorge
{
	public static class PgsqlContextProvider
	{
		public static IServiceCollection AddPgsqlContext(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString(Constants.MAIN_CONNECTION_NAME);

			services.AddDbContext<IEfContext, EfPgsqlContext>(options => options.UseNpgsql(connectionString));
			return services;
		}
	}
}
