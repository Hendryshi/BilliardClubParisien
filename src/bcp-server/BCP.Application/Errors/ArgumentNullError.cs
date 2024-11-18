using FluentResults;

namespace BCP.Application.Errors
{
	public class ArgumentNullError : Error
	{
		public ArgumentNullError(string message) : base(message) { }
		public ArgumentNullError(string message, Error causeBy) : base(message, causeBy) { }
	}
}