using BCP.Domain.Entities;
using FluentResults;

namespace BCP.Application.Interfaces
{
    public interface ITokenService
    {
        Result<TokenResult> GenerateToken(User user);
        Result<string> GenerateRefreshToken();
    }

    public class TokenResult
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
} 