using FluentResults;

namespace BCP.Application.Errors
{
	public class ConfigErrorError : Error
	{
		public ConfigErrorError(string message) : base(message) { }
		public ConfigErrorError(string message, Error causeBy) : base(message, causeBy) { }
	}
}
