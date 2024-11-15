using BCP.Application.Commands.User.Create;
using BCP.Application.Commands.User.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Api.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]
	public class UserController : BaseApiController
	{
		public UserController(IMediator mediator, ILogger<BaseApiController> logger, IWebHostEnvironment env)
				: base(mediator, logger, env) { }

		[HttpPost("")]
		public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
		{
			var response = await SendAsync(request, cancellationToken);
			return MapToResult(response);
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginUserResponse>> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
		{
			var response = await SendAsync(request, cancellationToken);
			return MapToResult(response);
		}
	}
}
