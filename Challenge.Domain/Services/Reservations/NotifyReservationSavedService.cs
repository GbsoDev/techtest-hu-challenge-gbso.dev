using Challenge.Domain.Entities.Reservations;

namespace Challenge.Domain.Services.Reservations
{
	[Service]
	public class NotifyReservationSavedService
	{
		internal async Task NotifyAsync(Reservation reservation, CancellationToken token)
		{
			throw new NotImplementedException();
		}
	}
}
