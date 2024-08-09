using AutoMapper;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Exceptions;
using Challenge.Domain.Ports;
using Challenge.Domain.Resources;
using Challenge.Domain.Validations;
using MediatR;

namespace Challenge.Application.Features.Reservations.Queries
{
	public class ReservationByIdQueryHandler : IRequestHandler<ReservationByIdQuery, ReservationDto>
	{
		private readonly IMapper _mapper;
		private readonly Lazy<IReservationsRepository> _reservationsRepository;

		public ReservationByIdQueryHandler(IMapper mapper, Lazy<IReservationsRepository> reservationRepository)
		{
			_mapper = mapper;
			_reservationsRepository = reservationRepository;
		}

		public async Task<ReservationDto> Handle(ReservationByIdQuery request, CancellationToken cancellationToken)
		{
			if (!request.Id.IsNotNull() || !request.IsNotDefault())
			{
				throw new NullOrEmptyException(string.Format(ValidationMessages.EmptyOrNullData, nameof(request.Id)));
			}
			var reservations = await _reservationsRepository.Value.GetByIdAsync(request.Id, cancellationToken);
			if (!reservations.IsNotNull())
			{
				throw new NotFoundException(string.Format(ValidationMessages.NotFound, nameof(Reservation), nameof(request.Id)));
			}
			return _mapper.Map<ReservationDto>(reservations);
		}
	}
}
