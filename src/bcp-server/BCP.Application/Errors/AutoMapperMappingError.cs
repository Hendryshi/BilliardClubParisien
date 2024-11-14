using FluentResults;

namespace BCP.Application.Errors
{
	public class AutoMapperMappingError : Error
	{
		public AutoMapperMappingError(string message) : base(message) { }
		public AutoMapperMappingError(string message, Error causeBy) : base(message, causeBy) { }
	}
}
