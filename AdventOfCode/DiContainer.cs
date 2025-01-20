using AdventOfCode._2015;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;

namespace AdventOfCode;
internal static class DiContainer
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddLogging(context);
        services.AddSingleton<Day1>();
    }

    private static IServiceCollection AddLogging(this IServiceCollection services, HostBuilderContext context)
    {
        var nLogProviderOptions = new NLogProviderOptions();
        var nLogConfiguration =
            (LoggingConfiguration)new NLogLoggingConfiguration(context.Configuration.GetSection("Logging:NLog"));
        var nLogFactory = new LogFactory(nLogConfiguration);
        var nLogProvider = new NLogLoggerProvider(nLogProviderOptions, nLogFactory);

        services.AddSingleton<ILoggerFactory>(serviceProvider => new NLogLoggerFactory(nLogProvider));

        return services;
    }
}
