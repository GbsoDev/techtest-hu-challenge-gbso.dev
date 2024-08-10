using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.EfStorage.Repositories
{
	[Repository]
	public class ReservationsRepository : IReservationsRepository
	{
		private readonly ILogger<ReservationsRepository> logger;
		private readonly IRepository<Reservation> _repository;

		public ReservationsRepository(ILogger<ReservationsRepository> logger, IRepository<Reservation> repository)
		{
			this.logger = logger;
			_repository = repository;
		}

		public async Task<Reservation> SaveAsync(Reservation entity, CancellationToken cancellationToken = default)
		{
			return await _repository.SaveAsync(entity, cancellationToken);
		}

		public async Task<Reservation[]> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _repository.GetAllAsync(cancellationToken);
		}

		public async Task<Reservation[]> GetMultipleByIdAsync(Guid[] ids, CancellationToken cancellationToken = default)
		{
			return await _repository.Set().Where(reservation => ids.Contains(reservation.Id)).ToArrayAsync(cancellationToken);
		}

		public async Task<Reservation?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			return await _repository.GetByIdAsync(id, cancellationToken);
		}

		public async Task<Reservation> UpdateAsync(Reservation entity, CancellationToken cancellationToken = default)
		{
			return await _repository.UpdateAsync(entity, cancellationToken);
		}

		public async Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			return await _repository.DeleteByIdAsync(id, cancellationToken);
		}

		public async Task<bool> DeleteAsync(Reservation entity, CancellationToken cancellationToken = default)
		{
			return await _repository.DeleteAsync(entity, cancellationToken);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _repository.SaveChangesAsync(cancellationToken);
		}
	}
}
