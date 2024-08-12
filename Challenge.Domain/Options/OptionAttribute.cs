namespace Challenge.Domain.Options
{
	[AttributeUsage(AttributeTargets.Class)]
	public class OptionAttribute : Attribute
	{
		public string SecctionName { get; }

		public OptionAttribute(string secctionName)
		{
			SecctionName = secctionName;
		}
	}
}
