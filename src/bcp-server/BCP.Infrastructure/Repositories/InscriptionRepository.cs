using BCP.Application.Exceptions;
using BCP.Application.Interfaces;
using BCP.Application.Services.Helpers;
using BCP.Domain.Entities;
using BCP.Infrastructure.Persistence;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BCP.Infrastructure.Repositories
{

	public class InscriptionRepository : IInscriptionRepository
	{
		private AppDbContext _dbContext;

		public InscriptionRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		#region Inscription CRUD
		public async Task<Result<Inscription>> AddAsync(Inscription oobject)
		{
			try
			{
				ArgumentNullException.ThrowIfNull(oobject);
				oobject.DtCreate = DateTime.Now;
				oobject.DtUpdate = DateTime.Now;
				var response = await _dbContext.AddAsync(oobject);
				return Result.Ok(response.Entity);
			}
			catch(Exception e)
			{
				return ResultHelper.MapToResult(e);
			}
		}

		public async Task<Result<Inscription>> GetAsync(int id)
		{
			try
			{
				var response = await _dbContext.Inscriptions
					.AsNoTracking()
					.FirstOrDefaultAsync(u => u.Id == id);

				if(response == null) 
					throw new NotFoundException($"The inscription of id {id} does not exist.");

				return Result.Ok(response);
			}
			catch(Exception e)
			{
				return ResultHelper.MapToResult(e);
			}
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public async Task<Result<Inscription>> UpdateAsync(Inscription oobject)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			try
			{
				ArgumentNullException.ThrowIfNull(oobject);
				oobject.DtUpdate = DateTime.Now;
				var response = _dbContext.Inscriptions.Update(oobject);

				return Result.Ok(response.Entity);
			}
			catch(Exception e)
			{
				return ResultHelper.MapToResult(e);
			}
		}

		public async Task<Result<Inscription>> GetByNameAsync(string firstName, string lastName, CancellationToken cancellationToken)
		{
			try
			{
				var inscription = await _dbContext.Inscriptions
					.AsNoTracking()
					.FirstOrDefaultAsync(u => u.FirstName.ToLower() == firstName.ToLower() && u.LastName.ToLower() == lastName.ToLower(), cancellationToken);

				return Result.Ok(inscription);
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
