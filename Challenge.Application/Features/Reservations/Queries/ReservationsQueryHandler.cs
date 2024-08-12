using AutoMapper;
using Challenge.Domain.Ports;
using MediatR;

namespace Challenge.Application.Features.Reservations.Queries
{
	public class ReservationsQueryHandler : IRequestHandler<ReservationsQuery, IEnumerable<ReservationDto>>
	{
		private readonly IMapper _mapper;
		private readonly Lazy<IReservationsRepository> _reservationsRepository;

		public ReservationsQueryHandler(IMapper mapper, Lazy<IReservationsRepository> reservationRepository)
		{
			_mapper = mapper;
			_reservationsRepository = reservationRepository;
		}

		public async Task<IEnumerable<ReservationDto>> Handle(ReservationsQuery request, CancellationToken cancellationToken)
		{
			var reservations = await _reservationsRepository.Value.GetAllAsync(cancellationToken);
			return _mapper.Map<ReservationDto[]>(reservations);
		}
	}
}
