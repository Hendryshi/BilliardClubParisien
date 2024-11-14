using BCP.Application.Exceptions;
using BCP.Application.Interfaces;
using BCP.Application.Services.Helpers;
using BCP.Domain.Entities;
using BCP.Infrastructure.Persistence;
using FluentResults;

namespace BCP.Infrastructure.Repositories
{

	public class UserRepository : IUserRepository
	{
		private AppDbContext _dbContext;

		public UserRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		#region BaseTemplate CRUD
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
				var response = await _dbContext.Users.FindAsync(id);
				_dbContext.ChangeTracker.Clear();
				if(response == null) throw new NotFoundException($"The user of id {id} does not exist.");

				return FluentResults.Result.Ok(response);
			}
			catch(Exception e)
			{
				return ResultHelper.MapToResult(e);
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
