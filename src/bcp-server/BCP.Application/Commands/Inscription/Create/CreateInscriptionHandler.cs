using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BCP.Application.Interfaces;
using BCP.Application.Services.Helpers;
using BCP.Application.Services.Logging;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BCP.Application.Commands.Inscription.Create
{
	public class CreateInscriptionHandler : IRequestHandler<CreateInscriptionRequest, Result<CreateInscriptionResponse>>
	{
		private readonly IMapper _mapper;
		private readonly IInscriptionRepository _repo;
		private readonly ILogger<CreateInscriptionHandler> _logger;

		public CreateInscriptionHandler(IMapper mapper, IInscriptionRepository repo, ILogger<CreateInscriptionHandler> logger)
		{
			_mapper = mapper;
			_repo = repo;
			_logger = logger;
		}

		public async Task<Result<CreateInscriptionResponse>> Handle(CreateInscriptionRequest request, CancellationToken cancellationToken)
		{
			_logger.Debug($"Entering command handler {GetType().Name}");
			try
			{
				var entityExistResult = await _repo.GetByNameAsync(request.Data.FirstName, request.Data.LastName, cancellationToken);
				if(entityExistResult.IsFailed) return entityExistResult.ToResult();

				if(entityExistResult.Value != null)
					return Result.Fail($"Inscription with name {request.Data.FirstName} {request.Data.LastName} already exists");

				//Map the command to domain entity
				var entity = _mapper.Map<Domain.Entities.Inscription>(request.Data);

				//Call domain method
				var operationResult = await _repo.AddAsync(entity);
				if(operationResult.IsFailed)
					return operationResult.ToResult();
				else
				{
					var commitResult = await _repo.CommitOperationsAsync();
					if(commitResult.IsFailed) return commitResult;

					//return the response
					var response = new CreateInscriptionResponse()
					{
						Data = _mapper.Map<Responses.Inscription.InscriptionResponse>(operationResult.Value)
					};

					return Result.Ok(response);
				}
			}
			catch(Exception ex)
			{
				_logger.Error(ex, "Failed to create user");
				return ResultHelper.MapToResult(ex);
			}
		}
	}
}
