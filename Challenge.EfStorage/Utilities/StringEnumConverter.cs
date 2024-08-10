using Challenge.Domain.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Challenge.EfStorage.Utilities
{
	public class StringEnumConverter<TEnum> : ValueConverter<TEnum, string> where TEnum : struct, Enum
	{
		public StringEnumConverter()
			: base(
				enumerador => enumerador.ToString(),
				texto => texto.ToEnum<TEnum>())
		{
		}
	}
}
