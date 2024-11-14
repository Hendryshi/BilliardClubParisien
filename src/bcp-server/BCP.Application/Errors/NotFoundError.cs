using FluentResults;

namespace BCP.Application.Errors
{
	public class NotFoundError : Error
	{
		public NotFoundError(string message) : base(message) { }
		public NotFoundError(string message, Error causeBy) : base(message, causeBy) { }
	}
}
