namespace Challenge.Domain.Dtos
{
	public class EmailDto
	{
		public required string Email { get; init; }
		public required string Subject { get; init; }
		public required string Body { get; init; }
	}
}