using Challenge.Domain.Options;

namespace Challenge.Infrastructure.Options

{
	[Option("WebApi")]
	public class WebApiOptions
	{
		public CorsOptions[] AllowCors { get; private set; }

		public WebApiOptions()
		{
			AllowCors = Array.Empty<CorsOptions>();
		}
	}
}
