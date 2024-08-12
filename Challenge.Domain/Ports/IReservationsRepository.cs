using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.RepositoryActions;
using Challenge.Domain.StorageActions;

namespace Challenge.Domain.Ports
{
	public interface IReservationsRepository : ISave<Reservation>, IGet<Reservation>, IUpdate<Reservation>, IDelete<Reservation>, ISaveChange
	{
		Task<Reservation[]> GetMultipleByIdAndIncludeAnyAsync(Guid[] ids, CancellationToken cancellationToken = default);
	}
}
