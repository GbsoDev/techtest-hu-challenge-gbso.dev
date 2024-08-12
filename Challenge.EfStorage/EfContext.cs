using Challenge.Domain.Helpers;
using Challenge.EfStorage.Configurations;
using Challenge.EfStorage.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Challenge.EfStorage
{
	public abstract class EfContext<TContext> : DbContext, IEfContext
		where TContext : DbContext
	{
		public EfContext(DbContextOptions<TContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(EfStorageAssemblyHelper.GetEfStorageAssembly);
			ApplyConfigurations(modelBuilder);
			foreach (var entityType in modelBuilder.Model.GetEntityTypes()
				.Where(entityType => EntityHelper.IsIAuditableEntity(entityType.ClrType)))
			{
				modelBuilder.Entity(entityType.Name).Property<DateTime>(IEfContext.SAVE_DATE_PROPERTY_NAME)
					.IsRequired();

				modelBuilder.Entity(entityType.Name).Property<DateTime>(IEfContext.LAST_UPDATE_PROPERTY_NAME);
			}
		}

		protected abstract void ApplyConfigurations(ModelBuilder modelBuilder);
	}
}
