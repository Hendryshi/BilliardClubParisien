using FluentResults;

namespace BCP.Application.Errors
{
	/// <summary>
	/// Must be used to return status code 400 (Bad Request) to indicate nonspecific failure.
	/// This is the generic client-side error status, used when no other 4xx error code is appropriate.
	/// 4xx codes are used to tell the client that a fault has taken place on THEIR side. They should not retransmit the same request again, but fix the error first.
	/// </summary>
	public class BadRequestError : Error
	{
		public BadRequestError() : base()
		{
		}
		public BadRequestError(string message) : base(message)
		{
		}
		public BadRequestError(string message, Error causeBy) : base(message, causeBy)
		{
		}
	}
}
