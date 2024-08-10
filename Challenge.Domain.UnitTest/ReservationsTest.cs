using System.Text.Json;

namespace Challenge.Domain.UnitTest
{
	public class ReservationsTest
	{
		[Fact]
		public void IsNotDefault_ShouldReturnFalseForDefaultValue()
		{
			object? guid = null;
			string body = JsonSerializer.Serialize(guid);
			var x = JsonSerializer.Deserialize<Guid?>(body);
		}
	}
}
