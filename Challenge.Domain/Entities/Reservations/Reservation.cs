using Challenge.Domain.Entities.Flights;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Challenge.Domain.Entities.Reservations
{
	public class Reservation : DomainEntity<Guid>, IAuditableEntity
	{
		[JsonInclude]
		public override Guid Id { get => base.Id; protected set => base.Id = value; }
		[JsonInclude]
		public string UserName { get; private set; }
		[JsonInclude]
		public string PassportNumber { get; private set; }
		[JsonInclude]
		public string Email { get; private set; }
		[JsonInclude]
		public Guid FlightId { get; private set; }
		[JsonInclude]
		public string SeatNumber { get; private set; }

		[JsonInclude]
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
		[JsonConstructor]
		private Reservation()
		{
		}
#pragma warning restore CS8618

		public string JsonSerialize()
		{
			return JsonSerializer.Serialize(this);
		}

		internal void SetFlight(Flight flight)
		{
			Flight = flight;
		}
	}
}
