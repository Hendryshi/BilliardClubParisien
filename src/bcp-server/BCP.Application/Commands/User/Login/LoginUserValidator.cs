using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.Application.Commands.User.Login
{
	public class LoginUserValidator : AbstractValidator<LoginUserRequest>
	{
		public LoginUserValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
		}
	}
}