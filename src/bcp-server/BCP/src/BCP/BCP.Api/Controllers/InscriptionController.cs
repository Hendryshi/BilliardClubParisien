using BCP.Application.Commands.Inscription.Create;
using BCP.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class InscriptionController : ApiControllerBase
    {
        public InscriptionController(IMediator mediator, ILogger<ApiControllerBase> logger, IWebHostEnvironment env)
                : base(mediator, logger, env) { }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult<CreateInscriptionResponse>> Create([FromBody] CreateInscriptionRequest request, CancellationToken cancellationToken)
        {
            var response = await SendAsync(request, cancellationToken);
            return MapToResult(response);
        }

    }
}
