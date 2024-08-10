using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Challenge.EfStorage
{
	public interface IRepository<TEntity>
		where TEntity : class, IDomainEntity
	{
		DbSet<TEntity> Set();

		Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default);

		Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default);

		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
