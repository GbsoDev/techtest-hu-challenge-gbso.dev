namespace Challenge.Domain.Entities.Reservations
{
	public static class ReservationParameters
	{
		public const int UserNameMinLength = 4;
		public const int UserNameMaxLength = 50;

		public const int PassportNumberMinLength = 4;
		public const int PassportNumberMaxLength = 20;

		public const int SeatNumberMinLength = 3;
		public const int SeatNumberMaxLength = 3;

		public const string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
		public const int EmailMaxLength = 100;
	}
}
