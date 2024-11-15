using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

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
				RuleFor(x => x.Id).Empty();
				RuleFor(x => x.FirstName).NotEmpty();
				RuleFor(x => x.LastName).NotEmpty();
				RuleFor(x => x.Sex).NotEmpty();
				RuleFor(x => x.Email).NotEmpty();
				RuleFor(x => x.Status).NotEmpty();
			}
		}
	}
}
