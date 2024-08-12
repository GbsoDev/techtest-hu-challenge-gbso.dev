using Challenge.Domain.Resources;
using Challenge.Domain.Validations;

namespace Challenge.Domain.Entities.Reservations
{
	public static class ReservationValidator
	{
		public static ValidationSet ValidateToCreate(Reservation reservation)
		{
			var validationSet = new ValidationSet(string.Format(ValidationMessages.ActionRegisterDenied, nameof(Reservation)));

			validationSet.AddIsNotEmptyOrWhiteSpaceValidation(reservation.UserName,
				ValidationMessages.EmptyOrNullWithSpacesData, nameof(reservation.UserName));

			validationSet.AddLengthBetweenValidation(reservation.UserName, ReservationParameters.UserNameMinLength, ReservationParameters.UserNameMaxLength,
				ValidationMessages.CharacterRangeData, nameof(reservation.UserName), ReservationParameters.UserNameMinLength, ReservationParameters.UserNameMaxLength);

			validationSet.AddIsNotEmptyOrWhiteSpaceValidation(reservation.PassportNumber,
				ValidationMessages.EmptyOrNullWithSpacesData, nameof(reservation.PassportNumber));

			validationSet.AddLengthBetweenValidation(reservation.UserName, ReservationParameters.PassportNumberMinLength, ReservationParameters.PassportNumberMaxLength,
				ValidationMessages.CharacterRangeData, nameof(reservation.UserName), ReservationParameters.UserNameMinLength, ReservationParameters.UserNameMaxLength);

			validationSet.AddIsNotEmptyOrWhiteSpaceValidation(reservation.Email,
				ValidationMessages.EmptyOrNullWithSpacesData, nameof(reservation.Email));

			validationSet.AddValidation(reservation.Email, IsValidEmail,
				ValidationMessages.EmailFormat, nameof(reservation.Email), reservation.Email);

			validationSet.AddIsNotNullValidation(reservation.FlightId,
				ValidationMessages.NullData, nameof(reservation.FlightId));

			validationSet.AddIsNotDefaultValidation(reservation.FlightId,
				ValidationMessages.EmptyOrNullData, nameof(reservation.FlightId));

			validationSet.AddIsNotEmptyOrWhiteSpaceValidation(reservation.SeatNumber,
				ValidationMessages.EmptyOrNullWithSpacesData, nameof(reservation.SeatNumber));

			validationSet.AddLengthBetweenValidation(reservation.SeatNumber, ReservationParameters.SeatNumberMinLength, ReservationParameters.SeatNumberMaxLength,
				ValidationMessages.CharacterRangeData, nameof(reservation.SeatNumber), ReservationParameters.SeatNumberMinLength, ReservationParameters.SeatNumberMaxLength);

			return validationSet;
		}

		public static ValidationSet ValidateToUpdate(Reservation reservation)
		{
			return ValidateToCreate(reservation).SetErrorMessage(string.Format(ValidationMessages.ActionUpdateDenied, nameof(Reservation)))

				.AddIsNotNullValidation(reservation.Id,
					ValidationMessages.NullData, nameof(reservation.Id))

				.AddIsNotDefaultValidation(reservation.Id,
					ValidationMessages.EmptyOrNullData, nameof(reservation.Id));
		}

		private static bool IsValidEmail(string email)
		{
			var emailRegex = new System.Text.RegularExpressions.Regex(
				ReservationParameters.EmailRegex,
				System.Text.RegularExpressions.RegexOptions.IgnoreCase
			);
			return emailRegex.IsMatch(email);
		}
	}
}
