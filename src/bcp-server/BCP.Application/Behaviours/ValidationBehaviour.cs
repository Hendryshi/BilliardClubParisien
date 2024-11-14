using BCP.Application.Exceptions;
using BCP.Application.Services.Logging;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Common.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<TRequest> _logger;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.Debug($"Entering in method {System.Reflection.MethodBase.GetCurrentMethod().Name} of service {this.GetType().Name}");

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .Where(r => r.Errors.Count != 0)
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Count != 0)
                {
                    if(failures.Select(f => f.ErrorCode).Contains(StatusCodes.Status403Forbidden.ToString()))
                    {
                        throw new ForbiddenAccessException();
                    }
                    else
                        throw new BCP.Application.Exceptions.ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
