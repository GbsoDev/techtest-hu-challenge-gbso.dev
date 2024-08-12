﻿using System.ComponentModel.DataAnnotations;

namespace Challenge.Domain.Exceptions
{
	public class NotFoundException
		: ValidationException
	{
		public NotFoundException(string? message)
			: base(message)
		{
		}

		public NotFoundException(string message, params object[] messageParams)
			: base(string.Format(message, messageParams))
		{
		}

		public NotFoundException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}
	}
}
