using Common.Domain.Extensions;
using FluentValidation;

namespace BCP.Application.Commands.Job.GeneratePdfAndSend
{
    public class GeneratePdfAndSendValidator : AbstractValidator<GeneratePdfAndSendRequest>
    {
        public GeneratePdfAndSendValidator(IServiceProvider serviceProvider)
        {
            RuleFor(m => m.Id).NotEmpty().IsFormatGuid();
        }
    }
}
