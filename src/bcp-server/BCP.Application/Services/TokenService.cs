using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCP.Application.Interfaces;
using BCP.Application.Services.Helpers;
using BCP.Domain.Entities;
using FluentResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BCP.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Result<TokenResult> GenerateToken(User user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"] 
                    ?? throw new InvalidOperationException("JWT:SecretKey not configured"));

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Result.Ok(new TokenResult
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    ExpiresAt = tokenDescriptor.Expires.Value
                });
            }
            catch (Exception ex)
            {
                return ResultHelper.MapToResult(ex);
            }
        }

        public Result<string> GenerateRefreshToken()
        {
            try
            {
                var randomNumber = new byte[32];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return Result.Ok(Convert.ToBase64String(randomNumber));
            }
            catch (Exception ex)
            {
                return ResultHelper.MapToResult(ex);
            }
        }
    }
} 