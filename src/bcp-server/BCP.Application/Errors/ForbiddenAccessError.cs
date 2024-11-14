using FluentResults;

namespace BCP.Application.Errors
{
	public class ForbiddenAccessError : Error
	{
		public ForbiddenAccessError(string message) : base(message) { }
		public ForbiddenAccessError(string message, Error causeBy) : base(message, causeBy) { }
	}
}
