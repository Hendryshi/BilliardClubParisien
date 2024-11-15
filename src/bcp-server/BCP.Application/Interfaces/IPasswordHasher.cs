using FluentResults;

namespace BCP.Application.Interfaces
{
    public interface IPasswordHasher
    {
        Result<(byte[] Hash, byte[] Salt)> HashPassword(string password);
        Result<bool> VerifyPassword(string password, byte[] hash, byte[] salt);
    }
} 