using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.Application.Commands.Inscription.Update
{
	public class UpdateInscriptionValidator : AbstractValidator<UpdateInscriptionRequest>
	{
		public UpdateInscriptionValidator(IServiceProvider serviceProvider)
		{
			RuleFor(m => m.Data).NotNull().SetValidator(ActivatorUtilities.CreateInstance<InscriptionUpdateValidator>(serviceProvider));
		}

		public class InscriptionUpdateValidator : AbstractValidator<Models.InscriptionCommand>
		{
			public InscriptionUpdateValidator()
			{
				RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
				RuleFor(x => x.FirstName).Must(x => x != string.Empty).When(x => x.FirstName != null);
				RuleFor(x => x.LastName).Must(x => x != string.Empty).When(x => x.LastName != null);
				RuleFor(x => x.Sex).Must(x => x != string.Empty).When(x => x.Sex != null);
				RuleFor(x => x.Email).Must(x => x != string.Empty).When(x => x.Email != null);
				RuleFor(x => x.Status).Must(x => x != string.Empty).When(x => x.Status != null);
			}
		}
	}
}
