using BCP.Application.Responses.User;

namespace BCP.Application.Commands.User.Create
{
	public sealed record CreateUserResponse
	{
		public UserResponse Data { get; set; }
	}
}
