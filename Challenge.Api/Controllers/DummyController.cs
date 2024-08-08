using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DummyController : ControllerBase
	{
		private readonly ILogger<DummyController> _logger;

		public DummyController(ILogger<DummyController> logger)
		{
			_logger = logger;
		}
	}
}
