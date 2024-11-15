using AutoMapper;
using BCP.Application.Interfaces;
using BCP.Application.Services.Helpers;
using BCP.Application.Services.Logging;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BCP.Application.Commands.Inscription.Update
{
	public class UpdateInscriptionHandler : IRequestHandler<UpdateInscriptionRequest, Result<UpdateInscriptionResponse>>
	{
		private readonly IInscriptionRepository _repo;
		private readonly ILogger<UpdateInscriptionHandler> _logger;
		private readonly IMapper _mapper;

		public UpdateInscriptionHandler(IInscriptionRepository repo, ILogger<UpdateInscriptionHandler> logger, IMapper mapper)
		{
			_repo = repo;
			_logger = logger;
			_mapper = mapper;
		}
		public async Task<Result<UpdateInscriptionResponse>> Handle(UpdateInscriptionRequest request, CancellationToken cancellationToken)
		{
			_logger.Debug($"Entering command handler {GetType().Name}");
			try
			{
				var entityResult = await _repo.GetAsync(request.Data.Id);
				if(entityResult.IsFailed) return entityResult.ToResult();

				var entityToUpdate = _mapper.Map(request.Data, entityResult.Value);

				//Call domain method
				var operationResult = await _repo.UpdateAsync(entityToUpdate);

				if(operationResult.IsFailed)
					return operationResult.ToResult();
				else
				{
					var commitResult = await _repo.CommitOperationsAsync();
					if(commitResult.IsFailed) return commitResult;

					var response = new UpdateInscriptionResponse()
					{
						Data = _mapper.Map<Responses.Inscription.InscriptionResponse>(operationResult.Value)
					};

					return response.ToResult();
				}
			}
			catch(Exception ex)
			{
				_logger.Error(ex, "Failed to update inscription");
				return ResultHelper.MapToResult(ex);
			}
		}
	}
}
