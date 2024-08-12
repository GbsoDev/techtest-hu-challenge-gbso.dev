using Challenge.EfPostgreSqlStorge.Helpers;
using Challenge.EfStorage;
using Microsoft.EntityFrameworkCore;

namespace Challenge.EfPostgreSqlStorge
{
	public class EfPgsqlContext : EfContext<EfPgsqlContext>
	{
		public EfPgsqlContext(DbContextOptions<EfPgsqlContext> options) : base(options)
		{
		}

		protected override void ApplyConfigurations(ModelBuilder modelBuilder)
		{
			var assembly = EfPgsqlAssemblyHelper.GetEfPgsqlStorageAssembly;
			modelBuilder.ApplyConfigurationsFromAssembly(assembly!);
		}
	}
}
