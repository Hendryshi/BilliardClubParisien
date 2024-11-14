using BCP.Domain.Entities;
using FluentResults;

namespace BCP.Application.Interfaces
{
	public interface IUserRepository
	{
		Task<Result<User>> AddAsync(User oobject);
		Task<Result> CommitOperationsAsync();
		Task<Result<User>> GetAsync(int id);
	}
}