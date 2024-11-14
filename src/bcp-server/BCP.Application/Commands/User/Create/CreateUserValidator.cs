using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.Application.Commands.User.Create
{
	public class CreateUserValidator : AbstractValidator<CreateUserRequest>
	{
		public CreateUserValidator(IServiceProvider serviceProvider)
		{
			RuleFor(m => m.Data).NotNull().SetValidator(ActivatorUtilities.CreateInstance<UserCreateValidator>(serviceProvider));
		}

		public class UserCreateValidator : AbstractValidator<Models.UserCommand>
		{
			public UserCreateValidator()
			{
				RuleFor(x => x.Id).Empty();
				RuleFor(x => x.UserName).NotEmpty().WithMessage("must have username");
				RuleFor(x => x.Password).NotEmpty();
			}
		}
	}
}
