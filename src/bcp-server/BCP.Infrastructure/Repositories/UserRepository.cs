using BCP.Application.Exceptions;
using BCP.Application.Interfaces;
using BCP.Application.Services.Helpers;
using BCP.Domain.Entities;
using BCP.Infrastructure.Persistence;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BCP.Infrastructure.Repositories
{

	public class UserRepository : IUserRepository
	{
		private AppDbContext _dbContext;

		public UserRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		#region User CRUD
		public async Task<Result<User>> AddAsync(User oobject)
		{
			try
			{
				ArgumentNullException.ThrowIfNull(oobject);
				var response = await _dbContext.AddAsync(oobject);
				return Result.Ok(response.Entity);
			}
			catch(Exception e)
			{
				return ResultHelper.MapToResult(e);
			}
		}

		public async Task<Result<User>> GetAsync(int id)
		{
			try
			{
				var response = await _dbContext.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(u => u.Id == id);

				if(response == null) 
					throw new NotFoundException($"The user of id {id} does not exist.");

				return Result.Ok(response);
			}
			catch(Exception e)
			{
				return ResultHelper.MapToResult(e);
			}
		}

		public async Task<Result<User>> GetByNameAsync(string userName, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _dbContext.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);

				if (user == null)
				{
					return Result.Fail(new Error("User not found"));
				}

				return Result.Ok(user);
			}
			catch (Exception ex)
			{
				return Result.Fail(new Error(ex.Message));
			}
		}
		#endregion

		#region Common
		public async Task<Result> CommitOperationsAsync()
		{
			try
			{
				await _dbContext.SaveChangesAsync();
				return Result.Ok();
			}
			catch(Exception e)
			{
				return ResultHelper.MapToResult(e);
			}
		}
		#endregion
	}
}
