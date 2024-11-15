using BCP.Application.Commands.Inscription.Create;
using BCP.Application.Commands.Inscription.Update;
using BCP.Application.Queries.Inscription.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateTool.Application.Queries.Inscription.GetAll;

namespace BCP.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[Controller]")]
	public class InscriptionController : BaseApiController
	{
		public InscriptionController(IMediator mediator, ILogger<BaseApiController> logger, IWebHostEnvironment env)
				: base(mediator, logger, env) { }

		[AllowAnonymous]
		[HttpPost("")]
		public async Task<ActionResult<CreateInscriptionResponse>> Create([FromBody] CreateInscriptionRequest request, CancellationToken cancellationToken)
		{
			var response = await SendAsync(request, cancellationToken);
			return MapToResult(response);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetInscriptionResponse>> Get([FromRoute] int id, CancellationToken cancellationToken = default)
		{
			var request = new GetInscriptionRequest { Id = id };
			var response = await SendAsync(request, cancellationToken);
			return MapToResult(response);
		}

		[HttpGet("All")]
		public async Task<ActionResult<GetAllInscriptionsResponse>> GetAllInscriptions(CancellationToken cancellationToken = default)
		{
			var request = new GetAllInscriptionsRequest();
			var result = await SendAsync(request, cancellationToken);
			return MapToResult(result);
		}

		[HttpPatch("{id}")]
		public async Task<ActionResult<UpdateInscriptionResponse>> Patch([FromRoute] int id, [FromBody] UpdateInscriptionRequest command, CancellationToken cancellationToken = default)
		{
			if(command.Data is not null) command.Data.Id = id;
			var response = await SendAsync(command, cancellationToken);
			return MapToResult(response);
		}
	}
}
