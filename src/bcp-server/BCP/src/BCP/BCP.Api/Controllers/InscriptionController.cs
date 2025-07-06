using BCP.Application.Commands.Inscription.Create;
using BCP.Application.Commands.Inscription.GeneratePdf;
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

        [AllowAnonymous]
        [HttpPost("GeneratePdf/{id}")]
        [ProducesResponseType(typeof(ActionResult), 200)]
        public async Task<ActionResult> GeneratePdf([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new GenerateInscriptionPdfRequest() { Id = id };
            var response = await SendAsync(request, cancellationToken);
            if(response.IsSuccess && response.Value != null)
            {
                return response.Value;
            }
            return MapToResult(response);
        }
    }
}
