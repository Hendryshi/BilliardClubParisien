using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Common.Domain.Extensions;

namespace BCP.Application.Commands.Inscription.Create
{
	public class CreateInscriptionValidator : AbstractValidator<CreateInscriptionRequest>
	{
		public CreateInscriptionValidator(IServiceProvider serviceProvider)
		{
			RuleFor(m => m.Data).NotNull().SetValidator(ActivatorUtilities.CreateInstance<InscriptionCreateValidator>(serviceProvider));
		}

		public class InscriptionCreateValidator : AbstractValidator<Models.InscriptionCommand>
		{
			public InscriptionCreateValidator()
			{
                RuleFor(x => x.Id).Null();
                RuleFor(x => x.FirstName).MustHaveValueNotEmpty();
				RuleFor(x => x.LastName).MustHaveValueNotEmpty();
				RuleFor(x => x.Sex).MustHaveValueNotEmpty();
				RuleFor(x => x.Email).MustHaveValueNotEmpty();
				RuleFor(x => x.Status).MustHaveValueNotEmpty();
			}
		}
	}
}
