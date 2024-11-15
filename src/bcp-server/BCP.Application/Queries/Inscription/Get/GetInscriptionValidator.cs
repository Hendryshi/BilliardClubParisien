using FluentValidation;

namespace BCP.Application.Queries.Inscription.Get
{
    public class GetInscriptionValidator : AbstractValidator<GetInscriptionRequest>
    {
        public GetInscriptionValidator()
        {
            RuleFor(m => m.Id).NotEmpty().GreaterThan(0);
        }
    }
}
