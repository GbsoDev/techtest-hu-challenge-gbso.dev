using AutoMapper;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Services.Reservations;
using MediatR;

namespace Challenge.Application.Features.Reservations.Commands
{
	public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand>
	{
		private readonly IMapper _mapper;
		private readonly Lazy<AddNewReservationToOutboxService> _createReservationService;

		public CreateReservationCommandHandler(IMapper mapper, Lazy<AddNewReservationToOutboxService> createReservationService)
		{
			this._mapper = mapper;
			_createReservationService = createReservationService;
		}

		public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
		{
			var reservation = _mapper.Map<Reservation>(request);
			await _createReservationService.Value.AddAsync(reservation, cancellationToken);
		}
	}
}
