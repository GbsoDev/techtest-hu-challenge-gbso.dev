﻿namespace Challenge.Domain.Validations
{
	public class ValidationSet
	{
		public string? ErrorMessage { get; private set; }
		public bool IsValid => !Errors.Any();
		public List<ValidationError> Errors { get; }

		public ValidationSet(string? errorMessage)
			: this()
		{
			ErrorMessage = errorMessage;
		}

		public ValidationSet()
		{
			Errors = new List<ValidationError>();
		}

		public void AddError(string message)
		{
			Errors.Add(new ValidationError(message));
		}

		public void ValidateAndThrow()
		{
			if (!IsValid)
			{
				throw new ValidationSetException(this, ErrorMessage);
			}
		}

		public ValidationSet SetErrorMessage(string errorMessage)
		{
			ErrorMessage = errorMessage;
			return this;
		}
	}
}
