using FluentResults;
using MediatR;

namespace BCP.Application.Queries.Inscription.Get
{
    public class GetInscriptionRequest : IRequest<Result<GetInscriptionResponse>>
    {
        public int Id { get; set; }
    }
}
