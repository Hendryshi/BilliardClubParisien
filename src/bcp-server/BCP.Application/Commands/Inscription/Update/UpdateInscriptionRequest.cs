using BCP.Application.Commands.Inscription.Models;
using FluentResults;
using MediatR;

namespace BCP.Application.Commands.Inscription.Update
{
	public class UpdateInscriptionRequest : IRequest<Result<UpdateInscriptionResponse>>
	{
		public InscriptionCommand Data { get; set; }
	}
}
