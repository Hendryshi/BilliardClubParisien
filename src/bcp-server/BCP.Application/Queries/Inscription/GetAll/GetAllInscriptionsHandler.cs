using AutoMapper;
using BCP.Application.Interfaces;
using BCP.Application.Responses.Inscription;
using BCP.Application.Services.Helpers;
using BCP.Application.Services.Logging;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TemplateTool.Application.Queries.Inscription.GetAll
{
	public class GetAllInscriptionsHandler : IRequestHandler<GetAllInscriptionsRequest, Result<GetAllInscriptionsResponse>>
	{
		private readonly IMapper _mapper;
		private readonly IInscriptionRepository _repo;
		private readonly ILogger<GetAllInscriptionsHandler> _logger;

		public GetAllInscriptionsHandler(IInscriptionRepository repo, ILogger<GetAllInscriptionsHandler> logger, IMapper mapper)
		{
			_mapper = mapper;
			_repo = repo;
			_logger = logger;
		}
		public async Task<Result<GetAllInscriptionsResponse>> Handle(GetAllInscriptionsRequest request, CancellationToken cancellationToken)
		{
			_logger.Debug($"Entering command handler {this.GetType().Name}");
			try
			{
				var operationResult = await _repo.GetAllAsync();
				if(operationResult.IsFailed) return operationResult.ToResult();

				var response = new GetAllInscriptionsResponse()
				{
					Data = _mapper.Map<List<InscriptionResponse>>(operationResult.Value)
				};
				return response;
			}
			catch(Exception ex)
			{
				_logger.Error(ex, "Failed to get all inscriptions");
				return ResultHelper.MapToResult(ex);
			}
		}
	}
}
