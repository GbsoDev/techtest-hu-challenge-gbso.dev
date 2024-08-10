using Challenge.Domain.Entities.Flights;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Challenge.Domain.Entities.Reservations
{
	public class Reservation : DomainEntity<Guid>, IAuditableEntity
	{
		public string UserName { get; private set; }
		public string PassportNumber { get; private set; }
		public string Email { get; private set; }
		public Guid FlightId { get; private set; }
		public string SeatNumber { get; private set; }

		[JsonIgnore]
		public virtual Flight? Flight { get; private set; }

		public Reservation(string userName, string passportNumber, string email, Guid flightId, string seatNumber)
			: this(Guid.Empty, userName, passportNumber, email, flightId, seatNumber, ReservationValidator.ValidateToCreate)
		{
		}

		public Reservation(Guid id, string userName, string passportNumber, string email, Guid flightId, string seatNumber)
			: this(id, userName, passportNumber, email, flightId, seatNumber, ReservationValidator.ValidateToUpdate)
		{
		}

		private Reservation(Guid id, string userName, string passportNumber, string email, Guid flightId, string seatNumber, Func<Reservation, Validations.ValidationSet>? validateFunction)
		{
			Id = id;
			UserName = userName;
			PassportNumber = passportNumber;
			Email = email;
			FlightId = flightId;
			SeatNumber = seatNumber;
			validateFunction?.Invoke(this).ValidateAndThrow();
		}

#pragma warning disable CS8618
		private Reservation()
		{
		}
#pragma warning restore CS8618

		public string JsonSerialize()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}
