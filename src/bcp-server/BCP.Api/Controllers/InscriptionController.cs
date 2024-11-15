using BCP.Application.Commands.Inscription.Create;
using BCP.Application.Commands.User.Create;
using BCP.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[Controller]")]
	public class InscriptionController : BaseApiController
	{
		public InscriptionController(IMediator mediator, ILogger<BaseApiController> logger, IWebHostEnvironment env)
				: base(mediator, logger, env) { }

		[HttpPost("")]
		public async Task<ActionResult<CreateInscriptionResponse>> Create([FromBody] CreateInscriptionRequest request, CancellationToken cancellationToken)
		{
			var response = await SendAsync(request, cancellationToken);
			return MapToResult(response);
		}
	}
}
