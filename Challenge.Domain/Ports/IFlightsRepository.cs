using Challenge.Domain.Entities.Flights;
using Challenge.Domain.StorageActions;

namespace Challenge.Domain.Ports
{
	public interface IFlightsRepository : IGet<Flight>
	{
	}
}
