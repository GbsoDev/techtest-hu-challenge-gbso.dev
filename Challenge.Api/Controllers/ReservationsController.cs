using Challenge.Application.Features.Reservations.Commands;
using Challenge.Application.Features.Reservations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReservationsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ReservationsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateReservationAsync(CreateReservationCommand createReservatrionCommand, CancellationToken cancellationToken)
		{
			await _mediator.Send(createReservatrionCommand, cancellationToken);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> CancelReservationAsync(Guid id, CancellationToken cancellationToken)
		{
			await _mediator.Send(new CancelReservationCommand(id), cancellationToken);
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetReservationsAsync(CancellationToken cancellationToken)
		{
			var reservationsResult = await _mediator.Send(new ReservationsQuery(), cancellationToken);
			return Ok(reservationsResult);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetReservationByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			var reservationsResult = await _mediator.Send(new ReservationByIdQuery(id), cancellationToken);
			return Ok(reservationsResult);
		}

		[HttpPatch]
		public async Task<IActionResult> UpdateReservationAsync(UpdateReservationCommand updateReservationCommand, CancellationToken cancellationToken)
		{
			await _mediator.Send(updateReservationCommand, cancellationToken);
			return Ok();
		}
	}
}
