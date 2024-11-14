using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BCP.Application.Interfaces;
using BCP.Application.Services.Helpers;
using BCP.Application.Services.Logging;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BCP.Application.Commands.User.Create
{
	public class CreateUserHandler : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _repo;
		private readonly ILogger<CreateUserHandler> _logger;

		public CreateUserHandler(IMapper mapper, IUserRepository repo, ILogger<CreateUserHandler> logger)
		{
			_mapper = mapper;
			_repo = repo;
			_logger = logger;
		}

		public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
		{
			_logger.Debug($"Entering command handler {GetType().Name}");
			try
			{
				//Map the command to domain entity
				var entity = _mapper.Map<Domain.Entities.User>(request.Data);

				using(var hmac = new HMACSHA512())
				{
					entity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Data.Password));
					entity.PasswordSalt = hmac.Key;
				}

				//Call domain method
				var operationResult = await _repo.AddAsync(entity);
				if(operationResult.IsFailed)
					return operationResult.ToResult();
				else
				{
					var commitResult = await _repo.CommitOperationsAsync();
					if(commitResult.IsFailed) return commitResult;

					//return the response
					var response = new CreateUserResponse()
					{
						Data = _mapper.Map<Responses.User.UserResponse>(operationResult.Value)
					};

					return Result.Ok(response);
				}
			}
			catch(Exception ex)
			{
				_logger.Error(ex, "Failed to create user");
				return ResultHelper.MapToResult(ex);
			}
		}
	}
}
