using FluentResults;
using MediatR;

namespace BCP.Application.Commands.Job.GeneratePdfAndSend
{
    public class GeneratePdfAndSendRequest : IRequest<Result<GeneratePdfAndSendResponse>>
    {
        public string Id { get; set; }
    }
}
