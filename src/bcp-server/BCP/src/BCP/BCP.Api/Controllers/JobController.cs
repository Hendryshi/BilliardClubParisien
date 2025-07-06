using BCP.Application.Commands.Inscription.Create;
using BCP.Application.Commands.Inscription.GeneratePdf;
using BCP.Application.Commands.Job.GeneratePdfAndSend;
using BCP.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class JobController : ApiControllerBase
    {
        public JobController(IMediator mediator, ILogger<ApiControllerBase> logger, IWebHostEnvironment env)
                : base(mediator, logger, env) { }

        [AllowAnonymous]
        [HttpPost("GeneratePdfAndSend/{id}")]
        public async Task<ActionResult<GeneratePdfAndSendResponse>> GeneratePdfAndSend([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new GeneratePdfAndSendRequest() { Id = id };
            var response = await SendAsync(request, cancellationToken);
            return MapToResult(response);
        }
    }
}
