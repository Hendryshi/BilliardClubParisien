using Common.Domain.Extensions;
using FluentValidation;

namespace BCP.Application.Commands.Inscription.GeneratePdf
{
    public class GenerateInscriptionPdfValidator : AbstractValidator<GenerateInscriptionPdfRequest>
    {
        public GenerateInscriptionPdfValidator(IServiceProvider serviceProvider)
        {
            RuleFor(m => m.Id).NotEmpty().IsFormatGuid();
        }
    }
}
