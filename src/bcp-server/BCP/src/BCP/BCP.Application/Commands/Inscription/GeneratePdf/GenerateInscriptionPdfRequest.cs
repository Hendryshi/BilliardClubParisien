using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Application.Commands.Inscription.GeneratePdf
{
    public class GenerateInscriptionPdfRequest : IRequest<Result<FileStreamResult>>
    {
        public string Id { get; set; }
    }
}
