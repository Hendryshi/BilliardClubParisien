using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BCP.Application.Services.Logging;
using BCP.Application.Extensions;

namespace BCP.Api.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]
	public class BaseApiController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<BaseApiController> _logger;
		private readonly IWebHostEnvironment _env;

		public BaseApiController(IMediator mediator, ILogger<BaseApiController> logger, IWebHostEnvironment env)
		{
			_mediator = mediator;
			_logger = logger;
			_env = env;
		}

		protected async Task<T> SendAsync<T>(IRequest<T> command, CancellationToken cancellationToken)
		{
			return await _mediator.Send(command, cancellationToken);
		}

		protected ActionResult MapToResult<T>(Result<T> result)
		{
			if(result.IsFailed) return ErrorResponse(result);
			return Ok(result.Value);
		}

		private ObjectResult ErrorResponse<T>(Result<T> result)
		{
			_logger.Error(result.ToErrorMessages());
			string errMsg;
			if(_env.IsDevelopment())
			{
				errMsg = result.ToErrorMessages();
			}
			else
			{
				errMsg = result.ToShortErrorMessage();
			}
			return Problem(detail: errMsg, statusCode: result.GetStatusCode());
		}
	}
}
