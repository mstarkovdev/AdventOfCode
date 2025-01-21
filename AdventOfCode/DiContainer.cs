using AdventOfCode.Configuration;
using AdventOfCode.Solutions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using System.Reflection;

namespace AdventOfCode;
internal static class DiContainer
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddLogging(context);
        services.AddTransient<InputDataFetcher>();
        services.AddSingleton<PuzzleSolver>();

        services.AddSingleton(context.Configuration.GetSection("InputDataConfiguration").Get<InputDataConfiguration>());
        services.AddSingleton(context.Configuration.GetSection("CurrentPuzzleInfo").Get<CurrentPuzzleInfo>());

        RegisterDaySolutions(services, context.Configuration.GetSection("CurrentPuzzleInfo").Get<CurrentPuzzleInfo>());
    }

    private static void RegisterDaySolutions(IServiceCollection services, CurrentPuzzleInfo puzzleInfo)
    {
        var solutionType = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => typeof(IDaySolution).IsAssignableFrom(t) && t.Name == $"Year{puzzleInfo.Year}Day{puzzleInfo.Day}Solution");

        if (solutionType != null)
        {
            services.AddTransient(typeof(IDaySolution), solutionType);
        }
        else
        {
            throw new InvalidOperationException($"Solution for Year {puzzleInfo.Year} Day {puzzleInfo.Day} not found.");
        }
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
