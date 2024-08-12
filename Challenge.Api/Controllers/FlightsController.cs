using Challenge.Application.Features.Flights.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FlightsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public FlightsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetFlightsAsync(CancellationToken cancellationToken)
		{
			var flightsResult = await _mediator.Send(new FlightsQuery(), cancellationToken);
			return Ok(flightsResult);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetFlightByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			var flightsResult = await _mediator.Send(new FlightByIdQuery(id), cancellationToken);
			return Ok(flightsResult);
		}
	}
}
