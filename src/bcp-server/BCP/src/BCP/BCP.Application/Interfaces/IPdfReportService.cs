using BCP.Domain.Entities;
using FluentResults;

namespace BCP.Application.Interfaces
{
    public interface IPdfReportService
    {
        Task<Result<byte[]>> GenerateInscriptionPdfReport(Inscription inscription, CancellationToken cancellationToken = default);
    }
}