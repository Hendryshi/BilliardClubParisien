using FluentResults;
using MediatR;

namespace BCP.Application.Commands.User.Login
{
	public class LoginUserRequest : IRequest<Result<LoginUserResponse>>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}