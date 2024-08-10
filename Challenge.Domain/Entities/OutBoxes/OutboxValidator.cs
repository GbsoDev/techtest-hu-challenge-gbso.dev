using Challenge.Domain.Resources;
using Challenge.Domain.Validations;

namespace Challenge.Domain.Entities.OutBoxes
{
	public static class OutboxValidator
	{
		public static ValidationSet ValidateToCreate(Outbox outbox)
		{
			return new ValidationSet(string.Format(ValidationMessages.ActionRegisterDenied, nameof(Outbox)))

				.AddIsDefinedValidation(outbox.EventType,
					ValidationMessages.ValueNotAllowedData, nameof(outbox.EventType), outbox.EventType)

				.AddIsNotEmptyOrWhiteSpaceValidation(outbox.EventData,
					ValidationMessages.EmptyOrNullWithSpacesData, nameof(outbox.EventData));
		}
	}
}
