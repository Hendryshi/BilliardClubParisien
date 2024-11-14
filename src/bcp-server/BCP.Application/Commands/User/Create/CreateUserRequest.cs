using FluentResults;
using MediatR;
using BCP.Application.Commands.User.Models;

namespace BCP.Application.Commands.User.Create
{
    public class CreateUserRequest : IRequest<Result<CreateUserResponse>>
    {
        public UserCommand Data { get; set; }
    }
}
