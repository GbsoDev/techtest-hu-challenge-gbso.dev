using Challenge.Domain.Entities.Flights;
using Challenge.Domain.Ports;
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
			return await _repository.GetAllAsync(cancellationToken);
		}

		public async Task<Flight?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			return await _repository.GetByIdAsync(id, cancellationToken);
		}
	}
}
