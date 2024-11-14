using Microsoft.Extensions.Configuration;
using Serilog;

namespace BCP.Application.Serilog
{
    public static class SerilogConfiguration
    {
        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("serilog.config.json", optional: false, reloadOnChange: true)
                //Env variable can be used to overrride appsettings
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Return a logger wich can be used to log before the DI has been set and so that serilog service is available
        /// </summary>
        /// <returns></returns>
        public static ILogger CreateSerilogLogger()
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(BuildConfiguration())
                .CreateLogger();
        }
    }
}
