using AutoMapper;
using Challenge.Domain.Ports;
using MediatR;

namespace Challenge.Application.Features.Flights.Queries
{
	public class FlightsQueryHandler : IRequestHandler<FlightsQuery, IEnumerable<FlightDto>>
	{
		private readonly IMapper _mapper;
		private readonly Lazy<IFlightsRepository> _flightsRepository;

		public FlightsQueryHandler(IMapper mapper, Lazy<IFlightsRepository> flightRepository)
		{
			_mapper = mapper;
			_flightsRepository = flightRepository;
		}

		public async Task<IEnumerable<FlightDto>> Handle(FlightsQuery request, CancellationToken cancellationToken)
		{
			var flightsResult = await _flightsRepository.Value.GetAllAsync(cancellationToken);
			return _mapper.Map<FlightDto[]>(flightsResult);
		}
	}
}
