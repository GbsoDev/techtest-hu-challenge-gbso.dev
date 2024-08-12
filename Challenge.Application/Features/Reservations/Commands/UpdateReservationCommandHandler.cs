using AutoMapper;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Services.Reservations;
using MediatR;

namespace Challenge.Application.Features.Reservations.Commands
{
	public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
	{
		private readonly IMapper _mapper;
		private readonly Lazy<AddUpdateReservationToOutboxService> _updateReservationService;

		public UpdateReservationCommandHandler(IMapper mapper, Lazy<AddUpdateReservationToOutboxService> updateReservationService)
		{
			_mapper = mapper;
			_updateReservationService = updateReservationService;
		}

		public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
		{
			var reservation = _mapper.Map<Reservation>(request);
			await _updateReservationService.Value.UpdateAsync(reservation, cancellationToken);
		}
	}
}
