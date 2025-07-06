using BCP.Application.Interfaces;
using BCP.Application.Services;
using BCP.Domain.Configuration;
using Common.Application;
using Common.Application.Behaviours;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BCP.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreApplicationCommonServices(configuration);

            services.AddAutoMapper(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
            }, new[]
            {
                typeof(Common.Application.Mappings.CoreModelMapper).Assembly,
                typeof(ConfigureServices).Assembly
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                cfg.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>));
            });
            services.AddValidatorsFromAssembly(typeof(ConfigureServices).Assembly);

            services.AddScoped<IPdfReportService, PdfReportService>();
            services.AddScoped<IInscriptionJobService, InscriptionJobService>();
            services.AddMailService(configuration);

            return services;
        }

        public static IServiceCollection AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config => config.UsePostgreSqlStorage(c => c.UseNpgsqlConnection(configuration.GetConnectionString("DefaultSQLConnection"))));
            services.AddHangfireServer();

            return services;
        }

        public static IServiceCollection AddMailService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SMTPOptions>(config.GetSection("SMTP"));
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
