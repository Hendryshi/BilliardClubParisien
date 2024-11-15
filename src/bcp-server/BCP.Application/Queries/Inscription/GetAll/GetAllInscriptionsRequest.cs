using FluentResults;
using MediatR;

namespace TemplateTool.Application.Queries.Inscription.GetAll
{
    public class GetAllInscriptionsRequest : IRequest<Result<GetAllInscriptionsResponse>>
    {
    }
}
