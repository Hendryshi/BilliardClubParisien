using MediatR;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

			return services;
		}

	}
}
