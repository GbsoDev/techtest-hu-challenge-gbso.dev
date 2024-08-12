using Challenge.Domain.Entities;

namespace Challenge.Domain.StorageActions
{
	public interface IGet<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default);
		Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
	}
}
