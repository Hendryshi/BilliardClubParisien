using BCP.Domain.Entities;
using FluentResults;

namespace BCP.Application.Interfaces
{
	public interface IInscriptionRepository
	{
		Task<Result<Inscription>> AddAsync(Inscription oobject);
		Task<Result> CommitOperationsAsync();
		Task<Result<Inscription>> GetAsync(int id);
		Task<Result<Inscription>> GetByNameAsync(string firstName, string lastName, CancellationToken cancellationToken);
		Task<Result<Inscription>> UpdateAsync(Inscription oobject);
	}
}