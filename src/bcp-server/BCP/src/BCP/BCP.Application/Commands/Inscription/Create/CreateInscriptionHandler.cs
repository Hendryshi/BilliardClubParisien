using AutoMapper;
using BCP.Application.Interfaces;
using Common.Application.Services.Helpers;
using Common.Application.Services.Logging;
using FluentResults;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BCP.Application.Commands.Inscription.Create
{
    public class CreateInscriptionHandler : IRequestHandler<CreateInscriptionRequest, Result<CreateInscriptionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IInscriptionRepository _repo;
        private readonly IInscriptionJobService _jobService;
        private readonly ILogger<CreateInscriptionHandler> _logger;

        public CreateInscriptionHandler(IMapper mapper, IInscriptionRepository repo, IInscriptionJobService jobService, ILogger<CreateInscriptionHandler> logger)
        {
            _mapper = mapper;
            _repo = repo;
            _jobService = jobService;
            _logger = logger;
        }

        public async Task<Result<CreateInscriptionResponse>> Handle(CreateInscriptionRequest request, CancellationToken cancellationToken)
        {
            _logger.Debug($"Entering command handler {GetType().Name}");
            try
            {

                var entity = _mapper.Map<Domain.Entities.Inscription>(request.Data);

                //Call domain method
                var addInscriptionResult = await _repo.AddAsync(entity);
                if(addInscriptionResult.IsFailed) return addInscriptionResult.ToResult();
                var inscriptionObj = addInscriptionResult.Value;

                if(entity.InscriptionImages != null && entity.InscriptionImages.Count > 0)
                {
                    foreach(var image in entity.InscriptionImages)
                    {
                        image.IdInscription = inscriptionObj.Id;
                        await _repo.AddAsync(image);
                    }
                }

                var commitResult = await _repo.CommitOperationsAsync();
                if(commitResult.IsFailed) return commitResult;

                //return the response
                var response = new CreateInscriptionResponse()
                {
                    Data = _mapper.Map<Responses.Inscription.InscriptionResponse>(entity)
                };

                BackgroundJob.Enqueue(() => _jobService.GeneratePdfAndSendEmailAsync(inscriptionObj.Id));

                return Result.Ok(response);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Failed to create inscription");
                return ResultHelper.MapToResult(ex);
            }
        }
    }
}
