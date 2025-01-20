using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdventOfCode
{
    internal class Program
    {
        private static ILogger<Program> _logger;

        static void Main(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder
                .ConfigureAppConfiguration(ConfigureApplication)
                .ConfigureServices(DiContainer.Configure);
            var host = hostBuilder.Build();

            _logger = host.Services.GetService<ILogger<Program>>();
            _logger.LogInformation("Hello, world!");

            host.Run();
        }

        private static void ConfigureApplication(HostBuilderContext context, IConfigurationBuilder builder)
        {
            context.Configuration = builder
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
