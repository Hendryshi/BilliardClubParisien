using System.Security.Cryptography;
using BCP.Application.Interfaces;
using BCP.Application.Services.Helpers;
using FluentResults;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BCP.Application.Services
{
	public class PasswordHasher : IPasswordHasher
	{
		private const int SaltSize = 16; // 128 bits
		private const int HashSize = 32; // 256 bits
		private const int Iterations = 10000;

		public Result<(byte[] Hash, byte[] Salt)> HashPassword(string password)
		{
			try
			{
				ArgumentNullException.ThrowIfNull(password);

				// 生成盐值
				byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

				// 生成哈希
				byte[] hash = KeyDerivation.Pbkdf2(
					password: password,
					salt: salt,
					prf: KeyDerivationPrf.HMACSHA256,
					iterationCount: Iterations,
					numBytesRequested: HashSize);

				return Result.Ok((Hash: hash, Salt: salt));
			}
			catch(Exception ex)
			{
				return ResultHelper.MapToResult(ex);
			}
		}

		public Result<bool> VerifyPassword(string password, byte[] hash, byte[] salt)
		{
			try
			{
				ArgumentNullException.ThrowIfNull(password);
				ArgumentNullException.ThrowIfNull(hash);
				ArgumentNullException.ThrowIfNull(salt);

				// 验证哈希长度
				if(hash.Length != HashSize || salt.Length != SaltSize)
				{
					return Result.Fail("Invaid password");
				}

				// 使用相同的盐值和参数计算哈希
				byte[] computedHash = KeyDerivation.Pbkdf2(
					password: password,
					salt: salt,
					prf: KeyDerivationPrf.HMACSHA256,
					iterationCount: Iterations,
					numBytesRequested: HashSize);

				// 使用固定时间比较防止计时攻击
				if(!CryptographicOperations.FixedTimeEquals(computedHash, hash))
					return Result.Fail("Invalid password");

				return Result.Ok();
			}
			catch(Exception ex)
			{
				return ResultHelper.MapToResult(ex);
			}
		}
	}
}