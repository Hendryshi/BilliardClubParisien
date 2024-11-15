using FluentResults;
using MediatR;
using BCP.Application.Commands.Inscription.Models;

namespace BCP.Application.Commands.Inscription.Create
{
    public class CreateInscriptionRequest : IRequest<Result<CreateInscriptionResponse>>
    {
        public InscriptionCommand Data { get; set; }
    }
}
