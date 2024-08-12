using Challenge.Domain.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Challenge.EfStorage.Utilities
{
	public class StringNullableEnumConverter<TEnum> : ValueConverter<TEnum?, string?> where TEnum : struct, Enum
	{
		public StringNullableEnumConverter()
			: base(
				enumerador => Tostring(enumerador),
				texto => texto.ToNullableEnum<TEnum>())
		{
		}

		private static string? Tostring(TEnum? enumerador)
		{
			return enumerador?.ToString();
		}
	}
}
