using Challenge.Domain.Entities.OutBoxes;
using Challenge.Domain.RepositoryActions;
using Challenge.Domain.StorageActions;

namespace Challenge.Domain.Ports;

public interface IOutboxRepository : ISave<Outbox>, IUpdate<Outbox>, IDelete<Outbox>, ISaveChange
{
	Task<Outbox[]> GetAllProcessedAsync(CancellationToken cancellationToken = default);
	Task<Outbox[]> GetAllUnProcessedAsync(CancellationToken cancellationToken = default);
}
