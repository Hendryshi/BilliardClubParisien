using BCP.Application.Entity.Models;
using FluentResults;

namespace BCP.Application.Interfaces
{
    public interface IEmailService
    {
        Task<Result> SendEmail(EmailCommand command);
    }
}