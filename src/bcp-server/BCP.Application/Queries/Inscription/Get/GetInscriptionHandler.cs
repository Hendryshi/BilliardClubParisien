using AutoMapper;
using BCP.Application.Interfaces;
using BCP.Application.Responses.Inscription;
using BCP.Application.Services.Helpers;
using BCP.Application.Services.Logging;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BCP.Application.Queries.Inscription.Get
{
	public class GetInscriptionHandler : IRequestHandler<GetInscriptionRequest, Result<GetInscriptionResponse>>
	{
		private readonly IMapper _mapper;
		private readonly IInscriptionRepository _repo;
		private readonly ILogger<GetInscriptionHandler> _logger;

		public GetInscriptionHandler(IInscriptionRepository repo, ILogger<GetInscriptionHandler> logger, IMapper mapper)
		{
			_mapper = mapper;
			_repo = repo;
			_logger = logger;
		}
		public async Task<Result<GetInscriptionResponse>> Handle(GetInscriptionRequest request, CancellationToken cancellationToken)
		{
			_logger.Debug($"Entering command handler {this.GetType().Name}");
			try
			{
				var operationResult = await _repo.GetAsync(request.Id);
				if(operationResult.IsFailed) return operationResult.ToResult();

				var response = new GetInscriptionResponse()
				{
					Data = _mapper.Map<InscriptionResponse>(operationResult.Value)
				};
				return response;
			}
			catch(Exception ex)
			{
				_logger.Error(ex, "Failed to get inscription");
				return ResultHelper.MapToResult(ex);
			}
		}
	}
}
