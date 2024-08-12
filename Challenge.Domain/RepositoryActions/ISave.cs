using Challenge.Domain.Entities;

namespace Challenge.Domain.StorageActions
{
	public interface ISave<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
	}
}
