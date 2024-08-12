using AutoMapper;
using Challenge.Domain.Entities.Flights;
using Challenge.Domain.Exceptions;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Challenge.Domain.Validations;
using MediatR;

namespace Challenge.Application.Features.Flights.Queries
{
	public class FlightByIdQueryHandler : IRequestHandler<FlightByIdQuery, FlightDto>
	{
		private readonly IMapper _mapper;
		private readonly Lazy<IFlightsRepository> _flightsRepository;

		public FlightByIdQueryHandler(IMapper mapper, Lazy<IFlightsRepository> flightRepository)
		{
			_mapper = mapper;
			_flightsRepository = flightRepository;
		}

		public async Task<FlightDto> Handle(FlightByIdQuery request, CancellationToken cancellationToken)
		{
			if (!request.Id.IsNotNull() || !request.IsNotDefault())
			{
				throw new NullOrEmptyException(string.Format(ValidationMessages.EmptyOrNullData, nameof(request.Id)));
			}
			var flights = await _flightsRepository.Value.GetByIdAsync(request.Id, cancellationToken);
			if (!flights.IsNotNull())
			{
				throw new NotFoundException(string.Format(ValidationMessages.NotFound, nameof(Flight), nameof(request.Id)));
			}
			return _mapper.Map<FlightDto>(flights);
		}
	}
}
