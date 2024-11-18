using FluentResults;

namespace BCP.Application.Errors
{
	public class ValidationError : Error
	{
		public ValidationError(string message) : base(message) { }
		public ValidationError(string message, Error causeBy) : base(message, causeBy) { }
	}
}