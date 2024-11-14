using MediatR;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Common.Application.Behaviours;
using FluentValidation;

namespace BCP.Application
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			});

			services.AddAutoMapper(cfg =>
			{
				cfg.AllowNullCollections = true;
				cfg.AllowNullDestinationValues = true;
			}, new[]
			{
				typeof(Entity.Mapping.MappingProfile).Assembly
			});
			services.AddValidatorsFromAssembly(typeof(BCP.Application.Entity.Mapping.MappingProfile).Assembly);

			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

			});

			return services;
		}

	}
}
