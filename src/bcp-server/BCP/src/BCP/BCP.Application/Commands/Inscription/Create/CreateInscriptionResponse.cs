using BCP.Application.Responses.Inscription;

namespace BCP.Application.Commands.Inscription.Create
{
	public sealed record CreateInscriptionResponse
	{
		public InscriptionResponse Data { get; set; }
	}
}
