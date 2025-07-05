using Common.Application.Interfaces;
using Common.Infrastructure;
using BCP.Application.Interfaces;
using BCP.Infrastructure.Persistence;
using BCP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureSqlServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServices(configuration);
            services.AddCommonInfraServices(configuration);
            return services;
        }

        public static IServiceCollection AddCommonInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreInfrastructureCommonServices(configuration);
            services.AddHttpContextAccessor();
            return services;
        }

        public static IServiceCollection AddSqlServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAsyncRepository, BaseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInscriptionRepository, InscriptionRepository>();

            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultSQLConnection");
                options.UseNpgsql(connectionString, m =>
                {
                    m.MigrationsHistoryTable("__EFMigrationsHistory");
                });
            });

            services.AddScoped<AppDbContext>();

            return services;
        }
    }
}
