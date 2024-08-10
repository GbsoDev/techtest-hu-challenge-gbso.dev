using Challenge.Domain.Entities;

namespace Challenge.Domain.StorageActions
{
	public interface IUpdate<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
	}
}