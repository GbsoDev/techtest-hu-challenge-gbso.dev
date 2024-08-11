namespace Challenge.Infrastructure.Options
{
	public class CorsOptions
	{
		public required string Name { get; init; }
		public required string Origin { get; init; }
		public required string[] Methods { get; init; }
	}
}
