using BCP.Application.Interfaces;
using BCP.Application.Services.Helpers;
using BCP.Application.Services.Logging;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BCP.Application.Commands.User.Login
{
	public class LoginUserHandler : IRequestHandler<LoginUserRequest, Result<LoginUserResponse>>
	{
		private readonly IUserRepository _userRepository;
		private readonly IPasswordHasher _passwordHasher;
		private readonly ITokenService _tokenService;
		private readonly ILogger<LoginUserHandler> _logger;

		public LoginUserHandler(
			IUserRepository userRepository,
			IPasswordHasher passwordHasher,
			ITokenService tokenService,
			ILogger<LoginUserHandler> logger)
		{
			_userRepository = userRepository;
			_passwordHasher = passwordHasher;
			_tokenService = tokenService;
			_logger = logger;
		}

		public async Task<Result<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
		{
			_logger.Debug($"Entering command handler {GetType().Name}");
			try
			{
				var userResult = await _userRepository.GetByNameAsync(request.UserName, cancellationToken);
				if(userResult.IsFailed) return userResult.ToResult();

				var user = userResult.Value;
				var verifyResult = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt);
				if(verifyResult.IsFailed) return verifyResult.ToResult();

				// 生成token
				var tokenResult = _tokenService.GenerateToken(user);
				if(tokenResult.IsFailed) return tokenResult.ToResult();

				var refreshTokenResult = _tokenService.GenerateRefreshToken();
				if(refreshTokenResult.IsFailed) return refreshTokenResult.ToResult();

				return Result.Ok(new LoginUserResponse
				{
					UserName = request.UserName,
					AccessToken = tokenResult.Value.AccessToken
				});
			}
			catch(Exception ex)
			{
				_logger.Error(ex, "Failed to login");
				return ResultHelper.MapToResult(ex);
			}
		}
	}
}