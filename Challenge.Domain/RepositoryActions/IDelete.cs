using Challenge.Domain.Entities;

namespace Challenge.Domain.RepositoryActions
{
	public interface IDelete<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
		Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default);
	}
}