using FluentResults;

namespace BCP.Application.Errors
{
	public class InvalidOperationError : Error
	{
		public InvalidOperationError(string message) : base(message) { }
		public InvalidOperationError(string message, Error causeBy) : base(message, causeBy) { }
	}
}
