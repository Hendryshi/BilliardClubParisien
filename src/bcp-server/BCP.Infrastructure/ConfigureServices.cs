using BCP.Application.Interfaces;
using BCP.Application.Services;
using BCP.Infrastructure.Repositories;
using BCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.Infrastructure
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultSQLConnection"), m =>
				{
					m.MigrationsHistoryTable("__EFMigrationsHistory", "BCP");
				});
			});

			services.AddTransient<IUserRepository, UserRepository>();

			return services;
		}
	}
}
