using AutoMapper;
using BCP.Application.Interfaces;
using Common.Application.Services.Helpers;
using Common.Application.Services.Logging;
using FluentResults;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BCP.Application.Commands.Job.GeneratePdfAndSend
{
    public class GeneratePdfAndSendHandler : IRequestHandler<GeneratePdfAndSendRequest, Result<GeneratePdfAndSendResponse>>
    {
        private readonly IInscriptionJobService _jobService;
        private readonly ILogger<GeneratePdfAndSendHandler> _logger;

        public GeneratePdfAndSendHandler(IInscriptionJobService jobService, ILogger<GeneratePdfAndSendHandler> logger)
        {
            _jobService = jobService;
            _logger = logger;
        }

        public async Task<Result<GeneratePdfAndSendResponse>> Handle(GeneratePdfAndSendRequest request, CancellationToken cancellationToken)
        {
            _logger.Debug($"Entering command handler {GetType().Name}");
            try
            {

                var jobResult = await _jobService.GeneratePdfAndSendEmailAsync(Guid.Parse(request.Id));
                if(jobResult.IsFailed) return jobResult;

                //return the response
                var response = new GeneratePdfAndSendResponse()
                {
                    Success = true
                };

                return Result.Ok(response);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Failed to run job GeneratePdfAndSend");
                return ResultHelper.MapToResult(ex);
            }
        }
    }
}
