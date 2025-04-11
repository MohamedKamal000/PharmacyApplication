using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfrastructureLayer.Logging;

public static class DataBaseLoggerExtensions
{
    public enum LogActionType
    {
        AdminAction,
        SystemAction
    }
    
    public static void LogAdminBehaviour<TCategory>(this ILogger logger,
        string message) where TCategory : notnull
    {
        logger.Log(
            LogLevel.Information,new EventId(1,LogActionType.AdminAction.ToString()),message,
            null,
            (e,f) => $"[Information],[{typeof(TCategory).Name}]: {e}");
    }

    public static void LogSystemBehaviour<TCategory>(this ILogger logger, 
        string message, LogLevel logLevel,Exception exception) where TCategory : notnull
    {
        logger.Log(
            logLevel,new EventId(1,LogActionType.SystemAction.ToString()),message,
            exception,
            (e,f) => $"[{logLevel.ToString()}],[{typeof(TCategory).Name}]: {e}\n" +
                     $"Exception Message:{e}");
    } 
    
    /*public static ILoggingBuilder AddDataBaseLogger(this ILoggingBuilder builder, 
        Action<DataBaseOptions> configure)
    {
        builder.Services.AddSingleton<ILoggerProvider, DataBaseLoggerProvider>();
        builder.Services.Configure(configure);

        return builder;
    }*/
}