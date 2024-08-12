using Challenge.Domain.Entities.OutBoxes;
using Challenge.Domain.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.EfStorage.Repositories
{
	[Repository]
	public class OutboxRepository : IOutboxRepository
	{
		private readonly ILogger<OutboxRepository> logger;
		private readonly IRepository<Outbox> _repository;

		public OutboxRepository(ILogger<OutboxRepository> logger, IRepository<Outbox> repository)
		{
			this.logger = logger;
			_repository = repository;
		}

		public async Task<Outbox> SaveAsync(Outbox entity, CancellationToken cancellationToken = default)
		{
			return await _repository.SaveAsync(entity, cancellationToken);
		}

		public async Task<Outbox[]> GetAllUnProcessedAsync(CancellationToken cancellationToken = default)
		{
			return await _repository.Set().OrderBy(outbox => outbox.EventData).Where(outbox => !outbox.Processed).ToArrayAsync(cancellationToken);
		}

		public async Task<Outbox[]> GetAllProcessedAsync(CancellationToken cancellationToken = default)
		{
			return await _repository.Set().OrderBy(outbox => outbox.EventData).Where(outbox => outbox.Processed).ToArrayAsync(cancellationToken);
		}

		public async Task<Outbox> UpdateAsync(Outbox entity, CancellationToken cancellationToken = default)
		{
			return await _repository.UpdateAsync(entity, cancellationToken);
		}

		public async Task<bool> DeleteAsync(Outbox entity, CancellationToken cancellationToken = default)
		{
			return await _repository.DeleteAsync(entity, cancellationToken);
		}

		public async Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			return await _repository.DeleteByIdAsync(id, cancellationToken);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _repository.SaveChangesAsync(cancellationToken);
		}
	}
}
