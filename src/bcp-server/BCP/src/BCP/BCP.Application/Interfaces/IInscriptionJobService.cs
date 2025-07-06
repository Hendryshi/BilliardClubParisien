using FluentResults;

namespace BCP.Application.Interfaces
{
    public interface IInscriptionJobService
    {
        Task<Result> GeneratePdfAndSendEmailAsync(Guid inscriptionId);
    }
}