using Challenge.Domain.Entities.Flights;
using Challenge.Domain.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.EfStorage.Repositories
{
	[Repository]
	public class FlightsRepository : IFlightsRepository
	{
		private readonly ILogger<FlightsRepository> logger;
		private readonly IRepository<Flight> _repository;

		public FlightsRepository(ILogger<FlightsRepository> logger, IRepository<Flight> repository)
		{
			this.logger = logger;
			_repository = repository;
		}

		public async Task<Flight[]> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _repository.Set()
				.Include(flight => flight.OriginCity)
				.Include(flight => flight.DestinationCity)
				.ToArrayAsync(cancellationToken);
		}

		public async Task<Flight?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			return await _repository.Set()
				.Include(flight => flight.OriginCity)
				.Include(flight => flight.DestinationCity)
				.FirstOrDefaultAsync(flight => flight.Id.Equals(id), cancellationToken);
		}

		public async Task<Flight[]> GetMultipleByIds(Guid[] flightIds, CancellationToken cancellationToken)
		{
			return await _repository.Set()
				.Where(flight => flightIds.Contains(flight.Id))
				.Include(flight => flight.OriginCity)
				.Include(flight => flight.DestinationCity)
				.ToArrayAsync(cancellationToken);
		}
	}
}
