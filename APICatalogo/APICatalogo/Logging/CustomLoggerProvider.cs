using System.Collections.Concurrent;

namespace APICatalogo.Logging;

public class CustomLoggerProvider : ILoggerProvider
{

    readonly CustomLoggerProviderConfiguration loggerConfig;

    readonly ConcurrentDictionary<string, CustomLogger> loggers = new ConcurrentDictionary<string, CustomLogger>();

    public CustomLoggerProvider(CustomLoggerProviderConfiguration loggerConfiguration)
    {
        this.loggerConfig = loggerConfiguration;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, name => new CustomLogger(name, loggerConfig));
    }

    public void Dispose()
    {
        loggers.Clear();
    }
}
